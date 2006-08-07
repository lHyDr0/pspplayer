#define XMB

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

using Noxa.Emulation.Psp.Audio;
using Noxa.Emulation.Psp.Bios;
using Noxa.Emulation.Psp.Cpu;
using Noxa.Emulation.Psp.Video;
using Noxa.Emulation.Psp.IO;
using Noxa.Emulation.Psp.IO.Input;

namespace Noxa.Emulation.Psp.Player
{
	class Instance : IEmulationInstance
	{
		protected Host _host;
		protected EmulationParameters _params;

#if XMB
		protected CrossMediaBar.Manager _xmb;
#else
		protected GamePicker.PickerDialog _picker;
#endif

		protected List<IComponentInstance> _instances = new List<IComponentInstance>();
		protected IAudioDriver _audio;
		protected IBios _bios;
		protected ICpu _cpu;
		protected List<IIODriver> _io = new List<IIODriver>();
		protected IVideoDriver _video;

		protected Thread _thread;
		protected bool _isCreated = false;
		protected bool _shutDown = false;
		protected InstanceState _state = InstanceState.Idle;
		protected AutoResetEvent _stateChangeEvent = new AutoResetEvent( false );

		public Instance( Host host, EmulationParameters parameters )
		{
			Debug.Assert( host != null );
			Debug.Assert( parameters != null );

			_host = host;
			_params = parameters;
		}

		public EmulationParameters Parameters
		{
			get
			{
				return _params;
			}
		}

		public IAudioDriver Audio
		{
			get
			{
				return _audio;
			}
		}

		public IBios Bios
		{
			get
			{
				return _bios;
			}
		}

		public ICpu Cpu
		{
			get
			{
				return _cpu;
			}
		}

		public ReadOnlyCollection<IIODriver> IO
		{
			get
			{
				return _io.AsReadOnly();
			}
		}

		public IVideoDriver Video
		{
			get
			{
				return _video;
			}
		}

		public InstanceState State
		{
			get
			{
				return _state;
			}
		}

		public event EventHandler StateChanged;

		protected void OnStateChanged()
		{
			EventHandler handler = this.StateChanged;
			if( handler != null )
				handler( this, EventArgs.Empty );
		}

		public bool Create()
		{
			Debug.Assert( _isCreated == false );
			if( _isCreated == true )
				return true;

			_shutDown = false;
			_state = InstanceState.Idle;

			// Try to create all the components
			Debug.Assert( _params.BiosComponent != null );
			Debug.Assert( _params.CpuComponent != null );
			if( _params.AudioComponent != null )
			{
				_audio = _params.AudioComponent.CreateInstance( this, _params[ _params.AudioComponent ] ) as IAudioDriver;
				_instances.Add( ( IComponentInstance )_audio );
			}
			_bios = _params.BiosComponent.CreateInstance( this, _params[ _params.BiosComponent ] ) as IBios;
			_instances.Add( ( IComponentInstance )_bios );
			_cpu = _params.CpuComponent.CreateInstance( this, _params[ _params.CpuComponent ] ) as ICpu;
			_instances.Add( ( IComponentInstance )_cpu );
			foreach( IComponent component in _params.IOComponents )
			{
				IIODriver driver = component.CreateInstance( this, _params[ component ] ) as IIODriver;
				_io.Add( driver );
				_instances.Add( ( IComponentInstance )driver );
				if( driver is IInputDevice )
					( driver as IInputDevice ).WindowHandle = _host.Player.Handle;
			}
			if( _params.VideoComponent != null )
			{
				_video = _params.VideoComponent.CreateInstance( this, _params[ _params.VideoComponent ] ) as IVideoDriver;
				_video.ControlHandle = _host.Player.ControlHandle;
				_instances.Add( ( IComponentInstance )_video );
			}

#if XMB
			_xmb = new CrossMediaBar.Manager( this, _host.Player.Handle, _host.Player.ControlHandle );
#else
#endif
			
			// Create thread
			_thread = new Thread( new ThreadStart( this.RuntimeThread ) );
			_thread.Name = "Host runtime thread";
			_thread.IsBackground = true;
			_thread.Start();

			_isCreated = true;

			return true;
		}

		public void Destroy()
		{
			Debug.Assert( _isCreated == true );
			if( _isCreated == false )
				return;

			// Destroy thread
			_shutDown = true;
			_stateChangeEvent.Set();
			if( _thread.Join( 1000 ) == false )
			{
				// Failed to wait, so kill
				_thread.Abort();
			}
			_thread = null;

#if XMB
			// Destroy XMB
			_xmb.Cleanup();
			_xmb = null;
#else
#endif

			// Destroy all the components
			foreach( IComponentInstance component in _instances )
			{
				if( component != null )
					component.Cleanup();
			}
			_instances.Clear();
			_audio = null;
			_bios = null;
			_cpu = null;
			_io.Clear();
			_video = null;

			_isCreated = false;
			_state = InstanceState.Idle;
			this.OnStateChanged();
		}

		public void Start()
		{
			Debug.Assert( _isCreated == true );
			if( _isCreated == false )
				return;

			switch( _state )
			{
				case InstanceState.Running:
					return;
				case InstanceState.Paused:
					this.Resume();
					return;
				case InstanceState.Debugging:
					// TODO: debugger hook on Start()
					return;
				case InstanceState.Crashed:
					// Tried to start after crashing? Should not be allowed!
					Debug.WriteLine( "Tried to Start() after crashing; calling Restart() instead" );
					this.Restart();
					return;
				case InstanceState.Ended:
					this.Restart();
					return;
			}

			_state = InstanceState.Running;
			_stateChangeEvent.Set();
			this.OnStateChanged();
		}

		public void Stop()
		{
			Debug.Assert( _isCreated == true );
			if( _isCreated == false )
				return;

			_state = InstanceState.Ended;
			_stateChangeEvent.Set();
			this.OnStateChanged();

			_cpu.PrintStatistics();

			this.Destroy();
		}

		public void Pause()
		{
			Debug.Assert( _isCreated == true );
			if( _isCreated == false )
				return;

			if( _state != InstanceState.Running )
				return;

			_state = InstanceState.Paused;
			_stateChangeEvent.Set();
			this.OnStateChanged();
		}

		public void Resume()
		{
			Debug.Assert( _isCreated == true );
			if( _isCreated == false )
				return;

			if( _state != InstanceState.Paused )
				return;

			_state = InstanceState.Running;
			_stateChangeEvent.Set();
			this.OnStateChanged();
		}

		public void Restart()
		{
			Debug.Assert( _isCreated == true );
			if( _isCreated == false )
				return;

			this.Stop();
			this.Create();
			this.Start();
		}

		public void LightReset()
		{
		}

		public delegate void DummyDelegate();

		public void SwitchToXmb()
		{
			Debug.WriteLine( "Instance: switching to XMB" );
			_video.Cleanup();
#if XMB
			_xmb.Enable();
#else
			DummyDelegate del = delegate()
			{
				_picker = new Noxa.Emulation.Psp.Player.GamePicker.PickerDialog( this );
				if( _picker.ShowDialog( _host.Player ) == System.Windows.Forms.DialogResult.OK )
				{
				}
				_picker = null;
			};
			_host.Player.Invoke( del );
#endif
		}

		public void SwitchToGame( Games.GameInformation game )
		{
			Debug.WriteLine( "Instance: switching to game " + game.Parameters.Title );
#if XMB
			_xmb.Disable();
#else
#endif
			_bios.Kernel.Game = game;
		}

		private void RuntimeThread()
		{
			try
			{
				this.SwitchToXmb();

				while( _shutDown == false )
				{
					switch( _state )
					{
						case InstanceState.Ended:
						case InstanceState.Idle:
						case InstanceState.Paused:
						case InstanceState.Crashed:
							_stateChangeEvent.WaitOne();
							break;
						case InstanceState.Debugging:
							// TODO: debugging runtime loop code
							break;
						case InstanceState.Running:
							if( _bios.Kernel.Game == null )
							{
#if XMB
								if( _xmb.IsEnabled == false )
#else
#endif
								{
									_cpu.PrintStatistics();
									Debug.WriteLine( "Instance: kernel game ended" );
									this.SwitchToXmb();
								}

								// Run XMB if not game set
#if XMB
								_xmb.Update();
								Thread.Sleep( 10 );
#else
								Thread.Sleep( 100 );
#endif
							}
							else
							{
								// Run the kernel
								_bios.Kernel.Execute();
							}
							break;
					}
				}
			}
			catch( ThreadAbortException )
			{
			}
			catch( ThreadInterruptedException )
			{
			}
		}
	}
}