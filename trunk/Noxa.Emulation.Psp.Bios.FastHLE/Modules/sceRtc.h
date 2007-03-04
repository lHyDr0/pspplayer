// ----------------------------------------------------------------------------
// PSP Player Emulation Suite
// Copyright (C) 2006 Ben Vanik (noxa)
// Licensed under the LGPL - see License.txt in the project root for details
// ----------------------------------------------------------------------------

#pragma once

#include "NoxaShared.h"
#include "ModulesShared.h"
#include "Module.h"

using namespace System;
using namespace System::Diagnostics;
using namespace Noxa::Emulation::Psp;
using namespace Noxa::Emulation::Psp::Bios;

namespace Noxa {
	namespace Emulation {
		namespace Psp {
			namespace Bios {
				namespace Modules {

					public ref class sceRtc : public Module
					{
					public:
						sceRtc( Kernel^ kernel ) : Module( kernel ) {}
						~sceRtc(){}

					public:
						property String^ Name { virtual String^ get() override { return "sceRtc"; } }

						//virtual void Start() override;
						//virtual void Stop() override;
						//virtual void Clear() override;

					internal:
						virtual void* QueryNativePointer( uint nid ) override;

					public: // ------ Implemented calls ------

						[BiosFunction( 0xC41C2853, "sceRtcGetTickResolution" )] [Stateless]
						// u32 sceRtcGetTickResolution(); (/rtc/psprtc.h:46)
						int sceRtcGetTickResolution();

						[BiosFunction( 0x3F7AD767, "sceRtcGetCurrentTick" )] [Stateless]
						// int sceRtcGetCurrentTick(u64 *tick); (/rtc/psprtc.h:54)
						int sceRtcGetCurrentTick( IMemory^ memory, int tick );

					public: // ------ Stubbed calls ------

						[NotImplemented]
						[BiosFunction( 0x4CFA57B0, "sceRtcGetCurrentClock" )] [Stateless]
						// int sceRtcGetCurrentClock(pspTime *time, int tz); (/rtc/psprtc.h:63)
						int sceRtcGetCurrentClock( IMemory^ memory, int time, int tz );

						[NotImplemented]
						[BiosFunction( 0xE7C27D1B, "sceRtcGetCurrentClockLocalTime" )] [Stateless]
						// int sceRtcGetCurrentClockLocalTime(pspTime *time); (/rtc/psprtc.h:71)
						int sceRtcGetCurrentClockLocalTime( IMemory^ memory, int time );

						[NotImplemented]
						[BiosFunction( 0x34885E0D, "sceRtcConvertUtcToLocalTime" )] [Stateless]
						// int sceRtcConvertUtcToLocalTime(const u64* tickUTC, u64* tickLocal); (/rtc/psprtc.h:80)
						int sceRtcConvertUtcToLocalTime( IMemory^ memory, int tickUTC, int tickLocal );

						[NotImplemented]
						[BiosFunction( 0x779242A2, "sceRtcConvertLocalTimeToUTC" )] [Stateless]
						// int sceRtcConvertLocalTimeToUTC(const u64* tickLocal, u64* tickUTC); (/rtc/psprtc.h:89)
						int sceRtcConvertLocalTimeToUTC( IMemory^ memory, int tickLocal, int tickUTC );

						[NotImplemented]
						[BiosFunction( 0x42307A17, "sceRtcIsLeapYear" )] [Stateless]
						// int sceRtcIsLeapYear(int year); (/rtc/psprtc.h:97)
						int sceRtcIsLeapYear( int year );

						[NotImplemented]
						[BiosFunction( 0x05EF322C, "sceRtcGetDaysInMonth" )] [Stateless]
						// int sceRtcGetDaysInMonth(int year, int month); (/rtc/psprtc.h:106)
						int sceRtcGetDaysInMonth( int year, int month );

						[NotImplemented]
						[BiosFunction( 0x57726BC1, "sceRtcGetDayOfWeek" )] [Stateless]
						// int sceRtcGetDayOfWeek(int year, int month, int day); (/rtc/psprtc.h:116)
						int sceRtcGetDayOfWeek( int year, int month, int day );

						[NotImplemented]
						[BiosFunction( 0x4B1B5E82, "sceRtcCheckValid" )] [Stateless]
						// int sceRtcCheckValid(const pspTime* date); (/rtc/psprtc.h:124)
						int sceRtcCheckValid( IMemory^ memory, int date );

						[NotImplemented]
						[BiosFunction( 0x3A807CC8, "sceRtcSetTime_t" )] [Stateless]
						// int sceRtcSetTime_t(pspTime* date, const time_t time); (/rtc/psprtc.h:244)
						int sceRtcSetTime_t( IMemory^ memory, int date, int time );

						[NotImplemented]
						[BiosFunction( 0x27C4594C, "sceRtcGetTime_t" )] [Stateless]
						// int sceRtcGetTime_t(const pspTime* date, time_t *time); (/rtc/psprtc.h:245)
						int sceRtcGetTime_t( IMemory^ memory, int date, int time );

						[NotImplemented]
						[BiosFunction( 0xF006F264, "sceRtcSetDosTime" )] [Stateless]
						// int sceRtcSetDosTime(pspTime* date, u32 dosTime); (/rtc/psprtc.h:246)
						int sceRtcSetDosTime( IMemory^ memory, int date, int dosTime );

						[NotImplemented]
						[BiosFunction( 0x36075567, "sceRtcGetDosTime" )] [Stateless]
						// int sceRtcGetDosTime(pspTime* date, u32 dosTime); (/rtc/psprtc.h:247)
						int sceRtcGetDosTime( IMemory^ memory, int date, int dosTime );

						[NotImplemented]
						[BiosFunction( 0x7ACE4C04, "sceRtcSetWin32FileTime" )] [Stateless]
						// int sceRtcSetWin32FileTime(pspTime* date, u64* win32Time); (/rtc/psprtc.h:248)
						int sceRtcSetWin32FileTime( IMemory^ memory, int date, int win32Time );

						[NotImplemented]
						[BiosFunction( 0xCF561893, "sceRtcGetWin32FileTime" )] [Stateless]
						// int sceRtcGetWin32FileTime(pspTime* date, u64* win32Time); (/rtc/psprtc.h:249)
						int sceRtcGetWin32FileTime( IMemory^ memory, int date, int win32Time );

						[NotImplemented]
						[BiosFunction( 0x7ED29E40, "sceRtcSetTick" )] [Stateless]
						// int sceRtcSetTick(pspTime* date, const u64* tick); (/rtc/psprtc.h:133)
						int sceRtcSetTick( IMemory^ memory, int date, int tick );

						[NotImplemented]
						[BiosFunction( 0x6FF40ACC, "sceRtcGetTick" )] [Stateless]
						// int sceRtcGetTick(const pspTime* date, u64 *tick); (/rtc/psprtc.h:142)
						int sceRtcGetTick( IMemory^ memory, int date, int tick );

						[NotImplemented]
						[BiosFunction( 0x9ED0AE87, "sceRtcCompareTick" )] [Stateless]
						// int sceRtcCompareTick(const u64* tick1, const u64* tick2); (/rtc/psprtc.h:151)
						int sceRtcCompareTick( IMemory^ memory, int tick1, int tick2 );

						[NotImplemented]
						[BiosFunction( 0x44F45E05, "sceRtcTickAddTicks" )] [Stateless]
						// int sceRtcTickAddTicks(u64* destTick, const u64* srcTick, u64 numTicks); (/rtc/psprtc.h:161)
						int sceRtcTickAddTicks( IMemory^ memory, int destTick, int srcTick, int numTicks );

						[NotImplemented]
						[BiosFunction( 0x26D25A5D, "sceRtcTickAddMicroseconds" )] [Stateless]
						// int sceRtcTickAddMicroseconds(u64* destTick, const u64* srcTick, u64 numMS); (/rtc/psprtc.h:171)
						int sceRtcTickAddMicroseconds( IMemory^ memory, int destTick, int srcTick, int numMS );

						[NotImplemented]
						[BiosFunction( 0xF2A4AFE5, "sceRtcTickAddSeconds" )] [Stateless]
						// int sceRtcTickAddSeconds(u64* destTick, const u64* srcTick, u64 numSecs); (/rtc/psprtc.h:181)
						int sceRtcTickAddSeconds( IMemory^ memory, int destTick, int srcTick, int numSecs );

						[NotImplemented]
						[BiosFunction( 0xE6605BCA, "sceRtcTickAddMinutes" )] [Stateless]
						// int sceRtcTickAddMinutes(u64* destTick, const u64* srcTick, u64 numMins); (/rtc/psprtc.h:191)
						int sceRtcTickAddMinutes( IMemory^ memory, int destTick, int srcTick, int numMins );

						[NotImplemented]
						[BiosFunction( 0x26D7A24A, "sceRtcTickAddHours" )] [Stateless]
						// int sceRtcTickAddHours(u64* destTick, const u64* srcTick, int numHours); (/rtc/psprtc.h:201)
						int sceRtcTickAddHours( IMemory^ memory, int destTick, int srcTick, int numHours );

						[NotImplemented]
						[BiosFunction( 0xE51B4B7A, "sceRtcTickAddDays" )] [Stateless]
						// int sceRtcTickAddDays(u64* destTick, const u64* srcTick, int numDays); (/rtc/psprtc.h:211)
						int sceRtcTickAddDays( IMemory^ memory, int destTick, int srcTick, int numDays );

						[NotImplemented]
						[BiosFunction( 0xCF3A2CA8, "sceRtcTickAddWeeks" )] [Stateless]
						// int sceRtcTickAddWeeks(u64* destTick, const u64* srcTick, int numWeeks); (/rtc/psprtc.h:221)
						int sceRtcTickAddWeeks( IMemory^ memory, int destTick, int srcTick, int numWeeks );

						[NotImplemented]
						[BiosFunction( 0xDBF74F1B, "sceRtcTickAddMonths" )] [Stateless]
						// int sceRtcTickAddMonths(u64* destTick, const u64* srcTick, int numMonths); (/rtc/psprtc.h:232)
						int sceRtcTickAddMonths( IMemory^ memory, int destTick, int srcTick, int numMonths );

						[NotImplemented]
						[BiosFunction( 0x42842C77, "sceRtcTickAddYears" )] [Stateless]
						// int sceRtcTickAddYears(u64* destTick, const u64* srcTick, int numYears); (/rtc/psprtc.h:242)
						int sceRtcTickAddYears( IMemory^ memory, int destTick, int srcTick, int numYears );

						[NotImplemented]
						[BiosFunction( 0xDFBC5F16, "sceRtcParseDateTime" )] [Stateless]
						// int sceRtcParseDateTime(u64 *destTick, const char *dateString); (/rtc/psprtc.h:251)
						int sceRtcParseDateTime( IMemory^ memory, int destTick, int dateString );

					};
				
				}
			}
		}
	}
}

/* GenerateStubsV2: auto-generated - CCEF238C */
