// ----------------------------------------------------------------------------
// PSP Player Emulation Suite
// Copyright (C) 2006 Ben Vanik (noxa)
// Licensed under the LGPL - see License.txt in the project root for details
// ----------------------------------------------------------------------------

#include "StdAfx.h"
//#include <Windows.h>
#include "R4000Memory.h"
#include <string>

using namespace System::Diagnostics;
using namespace System::IO;
using namespace Noxa::Emulation::Psp;
using namespace Noxa::Emulation::Psp::Cpu;

extern uint _managedMemoryReadCount;
extern uint _managedMemoryWriteCount;

R4000Memory::R4000Memory()
{
	MainMemory = ( byte* )_aligned_malloc( MainMemorySize, 16 );
	ScratchPad = ( byte* )_aligned_malloc( ScratchPadSize, 16 );
	FrameBuffer = ( byte* )_aligned_malloc( FrameBufferSize, 16 );

	memset( MainMemory, 0x0, MainMemorySize );
	memset( ScratchPad, 0x0, ScratchPadSize );
	memset( FrameBuffer, 0x0, FrameBufferSize );
}

R4000Memory::~R4000Memory()
{
	this->Clear();
}

R4000Memory::!R4000Memory()
{
	this->Clear();
}

void R4000Memory::Clear()
{
	if( MainMemory != NULL )
		_aligned_free( MainMemory );
	MainMemory = NULL;
	if( ScratchPad != NULL )
		_aligned_free( ScratchPad );
	ScratchPad = NULL;
	if( FrameBuffer != NULL )
		_aligned_free( FrameBuffer );
	FrameBuffer = NULL;
}

IMemorySegment^ R4000Memory::DefineSegment( MemoryType type, String^ name, int baseAddress, int length )
{
	throw gcnew NotSupportedException();
}

void R4000Memory::RegisterSegment( IMemorySegment^ segment )
{
	if( segment->BaseAddress == FrameBufferBase )
	{
		SAFEFREE( FrameBuffer );
		_frameBuffer = segment;
		//FrameBuffer = ptr?
		throw gcnew NotImplementedException( "IMemorySegment does not support grabbing of the frame buffer bytes" );
	}
	else
	{
		Debug::Assert( false, "Cannot override any segmenet but the framebuffer." );
	}
}

IMemorySegment^ R4000Memory::FindSegment( String^ name )
{
	throw gcnew NotSupportedException();
}

IMemorySegment^ R4000Memory::FindSegment( int baseAddress )
{
	throw gcnew NotSupportedException();
}

int R4000Memory::ReadWord( int address )
{
#ifdef STATISTICS
	_managedMemoryReadCount++;
#endif

	address &= 0x0FFFFFFF;

	//Debug::WriteLine( String::Format( "RW @ 0x{0:X8}", address ) );
	if( ( address >= MainMemoryBase ) && ( address < MainMemoryBound ) )
	{
		int* ptr = ( int* )( MainMemory + ( address - MainMemoryBase ) );
		return *ptr;
	}
	else if( ( address >= ScratchPadBase ) && ( address < ScratchPadBound ) )
	{
		int* ptr = ( int* )( ScratchPad + ( address - ScratchPadBase ) );
		return *ptr;
	}
	else if( ( address >= FrameBufferBase ) && ( address < FrameBufferBound ) )
	{
		if( _frameBuffer != nullptr )
			return _frameBuffer->ReadWord( address );
		else
		{
			int* ptr = ( int* )( FrameBuffer + ( address - FrameBufferBase ) );
			return *ptr;
		}
	}
	else
	{
		Debugger::Break();
		return 0;
	}
}

int64 R4000Memory::ReadDoubleWord( int address )
{
#ifdef STATISTICS
	_managedMemoryReadCount++;
#endif

	//Debug::WriteLine( String::Format( "RW @ 0x{0:X8}", address ) );
	if( ( address >= MainMemoryBase ) && ( address < MainMemoryBound ) )
	{
		int64* ptr = ( int64* )( MainMemory + ( address - MainMemoryBase ) );
		return *ptr;
	}
	else if( ( address >= ScratchPadBase ) && ( address < ScratchPadBound ) )
	{
		int64* ptr = ( int64* )( ScratchPad + ( address - ScratchPadBase ) );
		return *ptr;
	}
	else if( ( address >= FrameBufferBase ) && ( address < FrameBufferBound ) )
	{
		if( _frameBuffer != nullptr )
			return _frameBuffer->ReadDoubleWord( address );
		else
		{
			int64* ptr = ( int64* )( FrameBuffer + ( address - FrameBufferBase ) );
			return *ptr;
		}
	}
	else
	{
		Debugger::Break();
		return 0;
	}
}

array<byte>^ R4000Memory::ReadBytes( int address, int count )
{
	if( ( address >= MainMemoryBase ) && ( address < MainMemoryBound ) )
	{
		array<byte>^ buffer = gcnew array<byte>( count );
		pin_ptr<byte> ptr = &buffer[ 0 ];
		memcpy( ptr, MainMemory + ( address - MainMemoryBase ), count );
		return buffer;
	}
	else if( ( address >= ScratchPadBase ) && ( address < ScratchPadBound ) )
		throw gcnew NotImplementedException();
	else if( ( address >= FrameBufferBase ) && ( address < FrameBufferBound ) )
		throw gcnew NotImplementedException();
	else
		return nullptr;
}

int R4000Memory::ReadStream( int address, Stream^ destination, int count )
{
	if( ( address >= MainMemoryBase ) && ( address < MainMemoryBound ) )
	{
		//long pos = destination.Position;
		array<byte>^ buffer = gcnew array<byte>( count );
		destination->Write( buffer, 0, count );
		pin_ptr<byte> ptr = &buffer[ 0 ];
		memcpy( ptr, MainMemory + ( address - MainMemoryBase ), count );
		//destination.Position = pos;
		return count;
	}
	else if( ( address >= ScratchPadBase ) && ( address < ScratchPadBound ) )
		throw gcnew NotImplementedException();
	else if( ( address >= FrameBufferBase ) && ( address < FrameBufferBound ) )
		throw gcnew NotImplementedException();
	else
		return 0;
}

void R4000Memory::WriteWord( int address, int width, int value )
{
#ifdef STATISTICS
	_managedMemoryWriteCount++;
#endif

	if( ( address >= MainMemoryBase ) && ( address < MainMemoryBound ) )
	{
		byte* ptr = MainMemory + ( address - MainMemoryBase );
		switch( width )
		{
		case 4:
			{
				int* p = ( int* )ptr;
				*p = value;
			}
			break;
		case 1:
			*ptr = ( byte )value;
			break;
		case 2:
			{
				short* p = ( short* )ptr;
				*p = ( short )value;
			}
			break;
		default:
			Debug::Assert( false, "Unsupported width." );
			break;
		}
	}
	else if( ( address >= ScratchPadBase ) && ( address < ScratchPadBound ) )
	{
		byte* ptr = ScratchPad + ( address - ScratchPadBase );
		switch( width )
		{
		case 4:
			{
				int* p = ( int* )ptr;
				*p = value;
			}
			break;
		case 1:
			*ptr = ( byte )value;
			break;
		case 2:
			{
				short* p = ( short* )ptr;
				*p = ( short )value;
			}
			break;
		default:
			Debug::Assert( false, "Unsupported width." );
			break;
		}
	}
	else if( ( address >= FrameBufferBase ) && ( address < FrameBufferBound ) )
	{
		if( _frameBuffer != nullptr )
			_frameBuffer->WriteWord( address, width, value );
		else
		{
			byte* ptr = FrameBuffer + ( address - FrameBufferBase );
			switch( width )
			{
			case 4:
				{
					int* p = ( int* )ptr;
					*p = value;
				}
				break;
			case 1:
				*ptr = ( byte )value;
				break;
			case 2:
				{
					short* p = ( short* )ptr;
					*p = ( short )value;
				}
				break;
			default:
				Debug::Assert( false, "Unsupported width." );
				break;
			}
		}
	}
	else
	{
		Debugger::Break();
	}

	//if( this->MemoryChanged != nullptr )
	//this->MemoryChanged( this, address, width, value );
}

void R4000Memory::WriteDoubleWord( int address, int64 value )
{
#ifdef STATISTICS
	_managedMemoryWriteCount++;
#endif

	if( ( address >= MainMemoryBase ) && ( address < MainMemoryBound ) )
	{
		byte* ptr = MainMemory + ( address - MainMemoryBase );
		int64* p = ( int64* )ptr;
		*p = value;
	}
	else if( ( address >= ScratchPadBase ) && ( address < ScratchPadBound ) )
	{
		byte* ptr = ScratchPad + ( address - ScratchPadBase );
		int64* p = ( int64* )ptr;
		*p = value;
	}
	else if( ( address >= FrameBufferBase ) && ( address < FrameBufferBound ) )
	{
		if( _frameBuffer != nullptr )
			_frameBuffer->WriteDoubleWord( address, value );
		else
		{
			byte* ptr = FrameBuffer + ( address - FrameBufferBase );
			int64* p = ( int64* )ptr;
			*p = value;
		}
	}
	else
	{
		Debugger::Break();
	}

	//if( this->MemoryChanged != nullptr )
	//this->MemoryChanged( this, address, width, value );
}

void R4000Memory::WriteBytes( int address, array<byte>^ bytes )
{
	if( bytes->Length == 0 )
		return;
	if( ( address >= MainMemoryBase ) && ( address < MainMemoryBound ) )
	{
		pin_ptr<byte> ptr = &bytes[ 0 ];
		memcpy( MainMemory + ( address - MainMemoryBase ), ptr, bytes->Length );
	}
	else if( ( address >= ScratchPadBase ) && ( address < ScratchPadBound ) )
	{
		pin_ptr<byte> ptr = &bytes[ 0 ];
		memcpy( ScratchPad + ( address - ScratchPadBase ), ptr, bytes->Length );
	}
	else if( ( address >= FrameBufferBase ) && ( address < FrameBufferBound ) )
	{
		if( _frameBuffer != nullptr )
			_frameBuffer->WriteBytes( address, bytes );
		else
		{
			pin_ptr<byte> ptr = &bytes[ 0 ];
			memcpy( FrameBuffer + ( address - FrameBufferBase ), ptr, bytes->Length );
		}
	}
	else
		Debugger::Break();
}

void R4000Memory::WriteBytes( int address, array<byte>^ bytes, int index, int count )
{
	throw gcnew NotSupportedException();
}

void R4000Memory::WriteStream( int address, Stream^ source, int count )
{
	if( count == 0 )
		return;
	if( ( address >= MainMemoryBase ) && ( address < MainMemoryBound ) )
	{
		//long pos = source.Position;
		array<byte>^ buffer = gcnew array<byte>( count );
		source->Read( buffer, 0, count );
		pin_ptr<byte> ptr = &buffer[ 0 ];
		byte* bp = ptr;
		memcpy( MainMemory + ( address - MainMemoryBase ), bp, count );
		//source.Position = pos;

#if 0
		// DEBUG DUMP
		HANDLE f = CreateFileA( "dump.bin", GENERIC_WRITE, FILE_SHARE_READ, NULL, CREATE_ALWAYS, FILE_ATTRIBUTE_TEMPORARY, NULL );
		int dummy1;
		WriteFile( f, ( void* )( MainMemory + ( address - MainMemoryBase ) ), count, ( LPDWORD )&dummy1, NULL );
		CloseHandle( f );
#endif
	}
	else if( ( address >= ScratchPadBase ) && ( address < ScratchPadBound ) )
		throw gcnew NotImplementedException();
	else if( ( address >= FrameBufferBase ) && ( address < FrameBufferBound ) )
		throw gcnew NotImplementedException();
	else
		Debugger::Break();
}

void R4000Memory::Load( Stream^ stream )
{
	throw gcnew NotImplementedException();
}

void R4000Memory::Load( String^ fileName )
{
	Stream^ stream = nullptr;
	try
	{
		stream = File::OpenRead( fileName );
		this->Load( stream );
	}
	finally
	{
		stream->Close();
	}
}

void R4000Memory::Save( Stream^ stream )
{
	throw gcnew NotImplementedException();
}

void R4000Memory::Save( String^ fileName )
{
	Stream^ stream = nullptr;
	try
	{
		stream = File::OpenWrite( fileName );
		this->Save( stream );
	}
	finally
	{
		stream->Close();
	}
}

uint R4000Memory::GetMemoryHash( int address, int count, uint prime )
{
	if( ( address >= MainMemoryBase ) && ( address < MainMemoryBound ) )
	{
		uint hash = ( uint )count;
		byte *ptr = MainMemory + ( address - MainMemoryBase );
		for( int n = 0; n < count; n++ )
		{
			hash = hash + *ptr;
			ptr++;
		}
		return hash % prime;
	}
	else if( ( address >= ScratchPadBase ) && ( address < ScratchPadBound ) )
		throw gcnew NotImplementedException();
	else if( ( address >= FrameBufferBase ) && ( address < FrameBufferBound ) )
		throw gcnew NotImplementedException();
	else
		throw gcnew NotSupportedException();
}

void R4000Memory::DumpMainMemory( String^ fileName )
{
	if( MainMemory == NULL )
		return;

	FileStream^ stream = File::OpenWrite( fileName );
	BinaryWriter^ writer = gcnew BinaryWriter( stream );
	array<byte>^ bytes = gcnew array<byte>( MainMemorySize );
	pin_ptr<byte> ptr = &bytes[ 0 ];
	memcpy( ptr, MainMemory, MainMemorySize );
	writer->Write( bytes );
	writer->Close();
}