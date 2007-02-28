// ----------------------------------------------------------------------------
// PSP Player Emulation Suite
// Copyright (C) 2006 Ben Vanik (noxa)
// Licensed under the LGPL - see License.txt in the project root for details
// ----------------------------------------------------------------------------

#include "StdAfx.h"
#define WIN32_LEAN_AND_MEAN
#include <Windows.h>

#include "R4000Cpu.h"
#include "R4000BiosStubs.h"
#include "R4000Generator.h"

using namespace System;
using namespace Noxa::Emulation::Psp::Bios;
using namespace Noxa::Emulation::Psp::Cpu;

#pragma unmanaged

// sceRtc ----------------------------------------------
int sceRtcGetTickResolution();
void sceRtcGetCurrentTick( LARGE_INTEGER* address );

// scePower --------------------------------------------
//void scePowerTick(); <-- inlined
int scePowerSetClockFrequency( int cpuFreq, int ramFreq, int busFreq );
int scePowerSetBusClockFrequency( int busFreq );
int scePowerSetCpuClockFrequency( int cpuFreq );

// sceDisplayUser --------------------------------------
extern int sceDisplaySetFrameBuf( int address, int bufferWidth, int pixelFormat, int syncMode );
extern void sceDisplayWaitVblankStart();

// sceGeUser -------------------------------------------
//int sceGeEdramGetSize(); <-- inlined
//int sceGeEdramGetAddr(); <-- inlined
extern int sceGeListEnQueue( uint list, uint stall, int cbid, uint arg, int head );
extern int sceGeListDeQueue( int qid );
extern int sceGeListUpdateStallAddr( int qid, uint stall );
extern int sceGeListSync( int qid, int syncType );
extern int sceGeDrawSync( int syncType );

// sceUtilsForUser -------------------------------------
// All D(ata) and I(nstruction) cache inval calls are nop'ed

#pragma managed

bool R4000BiosStubs::EmitCall( R4000GenContext^ context, R4000Generator *g, int address, int nid )
{
	// Put the return value, if any, in EAX - if you don't return anything, put 0 just in case!

	switch( nid )
	{
	// sceRtc ----------------------------------------------
	case 0xc41c2853:		// sceRtcGetTickResolution
		g->call( ( int )sceRtcGetTickResolution );
		return true;
	case 0x3f7ad767:		// sceRtcGetCurrentTick
		g->mov( EAX, MREG( CTX, 4 ) );
		g->and( EAX, 0x3FFFFFFF );
		g->sub( EAX, MainMemoryBase );
		g->add( EAX, ( int )context->Memory->MainMemory );
		g->push( EAX );
		g->call( ( int )sceRtcGetCurrentTick );
		g->add( ESP, 4 );
		g->mov( EAX, 0 );
		return true;

	// scePower --------------------------------------------
	case 0xefd3c963:		// scePowerTick
		g->mov( EAX, 0 );
		return true;
	case 0x737486f2:		// scePowerSetClockFrequency
		g->push( MREG( CTX, 6 ) );
		g->push( MREG( CTX, 5 ) );
		g->push( MREG( CTX, 4 ) );
		g->call( ( int )scePowerSetClockFrequency );
		g->add( ESP, 12 );
		return true;
	case 0xb8d7b3fb:		// scePowerSetBusClockFrequency
		g->push( MREG( CTX, 4 ) );
		g->call( ( int )scePowerSetBusClockFrequency );
		g->add( ESP, 4 );
		return true;
	case 0x843fbf43:		// scePowerSetCpuClockFrequency
		g->push( MREG( CTX, 4 ) );
		g->call( ( int )scePowerSetCpuClockFrequency );
		g->add( ESP, 4 );
		return true;

	// sceUtilsForUser -------------------------------------
	case 0xbfa98062:		// sceKernelDcacheInvalidateRange
	case 0x79d1c3fa:		// sceKernelDcacheWritebackAll
	case 0xb435dec5:		// sceKernelDcacheWritebackInvalidateAll
	case 0x3ee30821:		// sceKernelDcacheWritebackRange
	case 0x34b9fa9e:		// sceKernelDcacheWritebackInvalidateRange
	case 0x920f104a:		// sceKernelIcacheInvalidateAll
	case 0xc2df770e:		// sceKernelIcacheInvalidateRange
		g->mov( EAX, 0 );
		return true;
	}

#ifdef NATIVEVIDEOINTERFACE
	// Note for video stuff: we must have a native video inteface for it to work!
	bool nativeVideoInterface = ( R4000Cpu::GlobalCpu->Emulator->Video->NativeInterface != IntPtr::Zero );

	if( nativeVideoInterface == true )
	{
		switch( nid )
		{
		// sceDisplayUser --------------------------------------
		case 0x289d82fe:		// sceDisplaySetFrameBuf
			g->push( MREG( CTX, 7 ) );
			g->push( MREG( CTX, 6 ) );
			g->push( MREG( CTX, 5 ) );
			g->push( MREG( CTX, 4 ) );
			g->call( ( int )sceDisplaySetFrameBuf );
			g->add( ESP, 16 );
			g->mov( EAX, 0 );
			return true;
		case 0x984c27e7:		// sceDisplayWaitVblankStart
			g->call( ( int )sceDisplayWaitVblankStart );
			g->mov( EAX, 0 );
			return true;

		// sceGeUser -------------------------------------------
		case 0x1f6752ad:		// sceGeEdramGetSize
			g->mov( EAX, FrameBufferSize );
			return true;
		case 0xe47e40e4:		// sceGeEdramGetAddr
			g-> mov( EAX, FrameBufferBase );
			return true;
		case 0xab49e76a:		// sceGeListEnQueue
			g->push( ( uint )0 ); // head = false
			g->push( MREG( CTX, 7 ) );
			g->push( MREG( CTX, 6 ) );
			g->push( MREG( CTX, 5 ) );
			g->push( MREG( CTX, 4 ) );
			g->call( ( int )sceGeListEnQueue );
			g->add( ESP, 20 );
			return true;
		case 0x1c0d95a6:		// sceGeListEnQueueHead
			g->push( ( uint )1 ); // head = true
			g->push( MREG( CTX, 7 ) );
			g->push( MREG( CTX, 6 ) );
			g->push( MREG( CTX, 5 ) );
			g->push( MREG( CTX, 4 ) );
			g->call( ( int )sceGeListEnQueue );
			g->add( ESP, 20 );
			return true;
		case 0x5fb86ab0:		// sceGeListDeQueue
			g->push( MREG( CTX, 4 ) );
			g->call( ( int )sceGeListDeQueue );
			g->add( ESP, 4 );
			return true;
		case 0xe0d68148:		// sceGeListUpdateStallAddr
			g->push( MREG( CTX, 5 ) );
			g->push( MREG( CTX, 4 ) );
			g->call( ( int )sceGeListUpdateStallAddr );
			g->add( ESP, 8 );
			return true;
		case 0x03444eb4:		// sceGeListSync
			g->push( MREG( CTX, 5 ) );
			g->push( MREG( CTX, 4 ) );
			g->call( ( int )sceGeListSync );
			g->add( ESP, 8 );
			return true;
		case 0xb287bd61:		// sceGeDrawSync
			g->push( MREG( CTX, 4 ) );
			g->call( ( int )sceGeDrawSync );
			g->add( ESP, 4 );
			return true;
		}
	}
#endif

	return false;
}

#pragma unmanaged

// sceRtc ----------------------------------------------

int sceRtcGetTickResolution()
{
	LARGE_INTEGER frequency;
	QueryPerformanceFrequency( &frequency );
	return ( uint )frequency.LowPart;
}

#if 1
void sceRtcGetCurrentTick( LARGE_INTEGER* address )
{
	QueryPerformanceCounter( address );
}
#else
// This alternate version will start the app at time 0
LARGE_INTEGER startTick;
void sceRtcGetCurrentTick( LARGE_INTEGER* address )
{
	if( startTick.QuadPart == 0 )
	{
		QueryPerformanceCounter( &startTick );
	}
	QueryPerformanceCounter( address );
	address->QuadPart -= startTick.QuadPart;
}
#endif

// scePower --------------------------------------------

//void scePowerTick(); <-- inlined

int scePowerSetClockFrequency( int cpuFreq, int ramFreq, int busFreq )
{
	bool valid =
		( cpuFreq >= 1 ) && ( cpuFreq <= 333 ) &&
		( ramFreq >= 1 ) && ( ramFreq <= 333 ) &&
		( busFreq >= 1 ) && ( busFreq <= 166 );
	if( valid == false )
		return -1;

	return 0;
}

int scePowerSetBusClockFrequency( int busFreq )
{
	if( ( busFreq < 1 ) || ( busFreq > 166 )
		return -1;
	return 0;
}

int scePowerSetCpuClockFrequency( int cpuFreq )
{
	if( ( cpuFreq < 1 ) || ( cpuFreq > 333 )
		return -1;
	return 0;
}

#pragma managed
