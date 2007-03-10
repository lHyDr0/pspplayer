#define DEFR( x )	GenerationResult x( R4000GenContext^ context, int pass, int address, uint code, byte opcode, byte rs, byte rt, byte rd, byte shamt, byte function );
#define DEFI( x )	GenerationResult x( R4000GenContext^ context, int pass, int address, uint code, byte opcode, byte rs, byte rt, ushort imm );
#define DEFJ( x )	GenerationResult x( R4000GenContext^ context, int pass, int address, uint code, byte opcode, uint imm );
#define DEFSP3( x )	GenerationResult x( R4000GenContext^ context, int pass, int address, uint code, byte rt, byte rd, byte function, ushort bshfl );
#define DEFFPU( x )	GenerationResult x( R4000GenContext^ context, int pass, int address, uint code, byte fmt, byte fs, byte ft, byte fd, byte function );

// Arithmetic
DEFR( SLL );
DEFR( SRL );
DEFR( SRA );
DEFR( SLLV );
DEFR( SRLV );
DEFR( SRAV );
DEFR( MOVZ );
DEFR( MOVN );
DEFR( MFHI );
DEFR( MTHI );
DEFR( MFLO );
DEFR( MTLO );
DEFR( MULT );
DEFR( MULTU );
DEFR( DIV );
DEFR( DIVU );
DEFR( MUL );		// don't know what the encoding of this is!
DEFR( MADD );
DEFR( MADDU );
DEFR( MSUB );
DEFR( MSUBU );
DEFR( ADD );
DEFR( ADDU );
DEFR( SUB );
DEFR( SUBU );
DEFR( AND );
DEFR( OR );
DEFR( XOR );
DEFR( NOR );
DEFR( SLT );
DEFR( SLTU );
DEFR( MAX );
DEFR( MIN );
DEFI( ADDI );
DEFI( ADDIU );
DEFI( SLTI );
DEFI( SLTIU );
DEFI( ANDI );
DEFI( ORI );
DEFI( XORI );
DEFI( LUI );
DEFSP3( EXT );
DEFSP3( INS );
DEFSP3( SEB );
DEFSP3( SEH );
DEFR( CLZ );
DEFR( CLO );

// Control
DEFR( JR );
DEFR( JALR );
DEFI( J );
DEFI( JAL );
DEFI( BEQ );
DEFI( BNE );
DEFI( BLEZ );
DEFI( BGTZ );
DEFI( BEQL );
DEFI( BNEL );
DEFI( BLEZL );
DEFI( BGTZL );
DEFJ( BLTZ );
DEFJ( BGEZ );
DEFJ( BLTZL );
DEFJ( BGEZL );
DEFJ( BLTZAL );
DEFJ( BGEZAL );
DEFJ( BLTZALL );
DEFJ( BGEZALL );

// Special
DEFR( SYSCALL );
DEFR( BREAK );
DEFR( SYNC );
DEFI( COP1 );
DEFI( COP2 );
DEFR( HALT );
DEFR( MFIC );
DEFR( MTIC );

// Memory
DEFI( LB );
DEFI( LH );
DEFI( LWL );
DEFI( LW );
DEFI( LBU );
DEFI( LHU );
DEFI( LWR );
DEFI( SB );
DEFI( SH );
DEFI( SWL );
DEFI( SW );
DEFI( SWR );
DEFI( CACHE );
DEFI( LL );
DEFI( LWCz );
DEFI( SC );
DEFI( SWCz );

// COP
DEFI( BCzF );
DEFI( BCzFL );
DEFI( BCzT );
DEFI( BCzTL );
DEFI( MFCz );
DEFI( MTCz );
DEFI( CFCz );
DEFI( CTCz );

// FPU
DEFFPU( FADD );
DEFFPU( FSUB );
DEFFPU( FMUL );
DEFFPU( FDIV );
DEFFPU( FSQRT );
DEFFPU( FABS );
DEFFPU( FMOV );
DEFFPU( FNEG );
DEFFPU( ROUNDL );
DEFFPU( TRUNCL );
DEFFPU( CEILL );
DEFFPU( FLOORL );
DEFFPU( ROUNDW );
DEFFPU( TRUNCW );
DEFFPU( CEILW );
DEFFPU( FLOORW );
DEFFPU( CVTS );
DEFFPU( CVTD );
DEFFPU( CVTW );
DEFFPU( CVTL );
DEFFPU( FCOMPARE );
