namespace SharpStationCore; 

public unsafe partial class Interpreter {
	public CpuState* State;

	void Branch(uint target) {
	}

	uint Copreg(uint cop, uint reg) => 0;
	void Copreg(uint cop, uint reg, uint value) {}

	uint Copcreg(uint cop, uint reg) => 0;
	void Copcreg(uint cop, uint reg, uint value) {}
	
	void Copfun(uint cop, uint command) {}

	T ReadMemory<T>(uint addr) => default;
	void WriteMemory<T>(uint addr, T value) {}

	void AbsorbMuldivDelay() {}
	
	void MulDelay(uint a, uint b, bool signed) {}
}