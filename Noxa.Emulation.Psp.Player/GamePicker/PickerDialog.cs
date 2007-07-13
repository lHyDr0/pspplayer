// ----------------------------------------------------------------------------
// PSP Player Emulation Suite
// Copyright (C) 2006 Ben Vanik (noxa)
// Licensed under the LGPL - see License.txt in the project root for details
// ----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Noxa.Emulation.Psp.Games;
using Noxa.Emulation.Psp.Media;

namespace Noxa.Emulation.Psp.Player.GamePicker
{
	partial class PickerDialog : Form
	{
		protected Instance _emulator;
		
		public PickerDialog()
		{
			InitializeComponent();
		}

		public PickerDialog( Instance emulator )
			: this()
		{
			Debug.Assert( emulator != null );

			_emulator = emulator;

			umdGameListing.Text = "UMDs";
			msGameListing.Text = "EBOOTs";

			SetEnabledState( true );

			this.FindGames();

			umdGameListing_SelectionChanged( umdGameListing, EventArgs.Empty );

			this.DialogResult = DialogResult.Cancel;
		}

		protected override void OnLoad( EventArgs e )
		{
			base.OnLoad( e );

			whidbeyTabControl1.SelectedIndex = 0;
		}

		protected override void OnClosed( EventArgs e )
		{
			umdGameListing.ClearGames();
			msGameListing.ClearGames();

			base.OnClosed( e );
		}

		public void LaunchGame( GameInformation game )
		{
			game.IgnoreDispose = true;
			if( game.HostPath != null )
			{
				// Need to have the UMD instance reload to the game
				string gamePath = game.HostPath;
				Debug.Assert( gamePath != null );

				_emulator.Umd.Eject();
				if( _emulator.Umd.Load( gamePath, false ) == false )
				{
					// Failed to load
				}

				// Regrab game info
				game = GameLoader.FindGame( _emulator.Umd );

				Properties.Settings.Default.LastPlayedGame = gamePath;
				Properties.Settings.Default.Save();
			}

			_emulator.SwitchToGame( game );
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		public void FindGames()
		{
			// Load the memory stick listing
			List<GameInformation> eboots = new List<GameInformation>();
			GameInformation[] ebootList = GameLoader.FindGames( _emulator.MemoryStick );
			foreach( GameInformation game in ebootList )
			{
				if( game.GameType == GameType.Eboot )
					eboots.Add( game );
			}
			msGameListing.AddGames( eboots );

			// Load recent UMDs
			if( Properties.Settings.Default.RecentGames != null )
			{
				string lastPlayed = Properties.Settings.Default.LastPlayedGame;
				List<GameInformation> umds = new List<GameInformation>();
				GameInformation selectedUmd = null;
				foreach( string gamePath in Properties.Settings.Default.RecentGames )
				{
					GameInformation game = this.LoadGameFromUmd( gamePath );
					if( game != null )
					{
						umds.Add( game );
						if( game.HostPath == lastPlayed )
							selectedUmd = game;
					}
				}
				umdGameListing.AddGames( umds );
				umdGameListing.SelectedGame = selectedUmd;
			}
		}

		private GameInformation LoadGameFromUmd( string gamePath )
		{
			//try
			{
				if( File.Exists( gamePath ) == false )
					return null;

				//Type deviceType = _emulator.Umd.Factory;
				//IComponent component = ( IComponent )Activator.CreateInstance( deviceType );
				//Debug.Assert( component != null );
				//if( component == null )
				//    throw new InvalidOperationException();

				//ComponentParameters parameters = new ComponentParameters();
				//parameters[ "path" ] = gamePath;
				//IUmdDevice umdDevice = component.CreateInstance( _emulator, parameters ) as IUmdDevice;
				//Debug.Assert( umdDevice != null );
				//if( umdDevice == null )
				//    throw new InvalidOperationException();

				IUmdDevice umdDevice = _emulator.Umd;
				if( umdDevice.Load( gamePath, true ) == false )
					return null;

				GameInformation game = GameLoader.FindGame( umdDevice );
				if( game != null )
				{
					game.HostPath = gamePath;
				}

				umdDevice.Cleanup();

				return game;
			}
			//catch
			{
				//Debug.Assert( false );
				//return null;
			}
		}

		private void SortRecentGames()
		{
			if( Properties.Settings.Default.RecentGames == null )
				return;

			List<GameInformation> games = new List<GameInformation>();
			foreach( string gamePath in Properties.Settings.Default.RecentGames )
			{
				GameInformation game = this.LoadGameFromUmd( gamePath );
				if( game != null )
					games.Add( game );
			}
			Comparison<GameInformation> del = delegate( GameInformation x, GameInformation y )
			{
				return string.Compare( x.Parameters.Title, y.Parameters.Title, true );
			};
			games.Sort( del );
			Properties.Settings.Default.RecentGames.Clear();
			foreach( GameInformation game in games )
				Properties.Settings.Default.RecentGames.Add( game.HostPath );
		}

		private void browseButton_Click( object sender, EventArgs e )
		{
			if( openFileDialog.ShowDialog( this ) == DialogResult.OK )
			{
				foreach( string gamePath in openFileDialog.FileNames )
				{
					GameInformation game = this.LoadGameFromUmd( gamePath );
					if( game != null )
					{
						if( Properties.Settings.Default.RecentGames == null )
							Properties.Settings.Default.RecentGames = new System.Collections.Specialized.StringCollection();
						bool exists = Properties.Settings.Default.RecentGames.Contains( gamePath );
						if( exists == false )
							Properties.Settings.Default.RecentGames.Add( gamePath );
						this.SortRecentGames();
						Properties.Settings.Default.Save();

						if( exists == false )
							umdGameListing.AddGame( game );

						umdGameListing_SelectionChanged( umdGameListing, EventArgs.Empty );
					}
				}
			}
		}

		private void removeButton_Click( object sender, EventArgs e )
		{
			AdvancedGameListing listing;
			if( whidbeyTabControl1.SelectedTab == whidbeyTabPage1 )
				listing = umdGameListing;
			else
				listing = msGameListing;
			GameInformation game = listing.SelectedGame;
			if( ( game == null ) ||
				( game.HostPath == null ) )
				return;

			string gamePath = game.HostPath;
			if( Properties.Settings.Default.RecentGames != null )
			{
				Properties.Settings.Default.RecentGames.Remove( gamePath );
				Properties.Settings.Default.Save();
			}

			listing.RemoveGame( game );

			umdGameListing_SelectionChanged( umdGameListing, EventArgs.Empty );
		}

		private void clearButton_Click( object sender, EventArgs e )
		{
			Properties.Settings.Default.RecentGames = new System.Collections.Specialized.StringCollection();
			Properties.Settings.Default.Save();

			AdvancedGameListing listing;
			if( whidbeyTabControl1.SelectedTab == whidbeyTabPage1 )
				listing = umdGameListing;
			else
				listing = msGameListing;
			listing.ClearGames();

			umdGameListing_SelectionChanged( umdGameListing, EventArgs.Empty );
		}

		private void playButton_Click( object sender, EventArgs e )
		{
			AdvancedGameListing listing;
			if( whidbeyTabControl1.SelectedTab == whidbeyTabPage1 )
				listing = umdGameListing;
			else
				listing = msGameListing;
			GameInformation game = listing.SelectedGame;
			if( game == null )
				return;

			this.LaunchGame( game );
		}

		private void cancelButton_Click( object sender, EventArgs e )
		{
		}

		private void SetEnabledState( bool browseEnabled )
		{
			browseButton.Enabled = browseEnabled;
			removeButton.Enabled = browseEnabled;
			clearButton.Enabled = browseEnabled;
		}

		private void whidbeyTabPage1_Showing( object sender, EventArgs e )
		{
			SetEnabledState( true );
			umdGameListing.Focus();
			umdGameListing_SelectionChanged( umdGameListing, EventArgs.Empty );
		}

		private void whidbeyTabPage2_Showing( object sender, EventArgs e )
		{
			SetEnabledState( false );
			msGameListing.Focus();
			msGameListing_SelectionChanged( msGameListing, EventArgs.Empty );
		}

		private void umdGameListing_SelectionChanged( object sender, EventArgs e )
		{
			removeButton.Enabled = ( umdGameListing.SelectedGame != null );
			clearButton.Enabled = umdGameListing.HasGames;
			playButton.Enabled = ( umdGameListing.SelectedGame != null );
		}

		private void msGameListing_SelectionChanged( object sender, EventArgs e )
		{
			playButton.Enabled = ( msGameListing.SelectedGame != null );
		}
	}
}