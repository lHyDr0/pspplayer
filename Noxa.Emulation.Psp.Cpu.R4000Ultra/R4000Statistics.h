// ----------------------------------------------------------------------------
// PSP Player Emulation Suite
// Copyright (C) 2006 Ben Vanik (noxa)
// Licensed under the LGPL - see License.txt in the project root for details
// ----------------------------------------------------------------------------

#pragma once

using namespace System;
using namespace Noxa::Emulation::Psp;

namespace Noxa {
	namespace Emulation {
		namespace Psp {
			namespace Cpu {

				ref class R4000Statistics : ICpuStatistics
				{
				public:
					R4000Statistics(){}

					property int InstructionsPerSecond
					{
						virtual int get()
						{
							return 0;
						}
					}
				};

			}
		}
	}
}
