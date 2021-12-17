// ReSharper disable CheckNamespace
#pragma warning disable CS0164
namespace DamageCore;

public partial class Disassembler {
    public static string Disassemble(Span<byte> insnBytes, ref ushort pc) {
		/* LD-rd-rs */
		if((insnBytes[0] & 0xC0) == 0x40) {
			var rd = (byte) ((byte) (insnBytes[0] >> 3) & 0x7);
			var rs = (byte) ((byte) (insnBytes[0] >> 0) & 0x7);
			if(((uint) (((rs) != (0x6U)) ? 1U : 0U)) == 0)
				goto insn_1;
			pc += 1;
			return (string) ("ld " + (string) (rd switch { (byte) (0x0U) => "B", (byte) (0x1U) => "C", (byte) (0x2U) => "D", (byte) (0x3U) => "E", (byte) (0x4U) => "H", (byte) (0x5U) => "L", (byte) (0x7U) => "A", _ => throw new NotImplementedException() }) + ", " + (string) (rs switch { (byte) (0x0U) => "B", (byte) (0x1U) => "C", (byte) (0x2U) => "D", (byte) (0x3U) => "E", (byte) (0x4U) => "H", (byte) (0x5U) => "L", (byte) (0x7U) => "A", _ => throw new NotImplementedException() }));
		}
		insn_1:
		/* LD-rd-imm8 */
		if((insnBytes[0] & 0xC7) == 0x6) {
			var rd = (byte) ((byte) (insnBytes[0] >> 3) & 0x7);
			var imm = (byte) ((byte) (insnBytes[1] >> 0) & 0xFF);
			if(((uint) (((rd) != (0x6U)) ? 1U : 0U)) == 0)
				goto insn_2;
			pc += 2;
			return (string) ("ld " + (string) (rd switch { (byte) (0x0U) => "B", (byte) (0x1U) => "C", (byte) (0x2U) => "D", (byte) (0x3U) => "E", (byte) (0x4U) => "H", (byte) (0x5U) => "L", (byte) (0x7U) => "A", _ => throw new NotImplementedException() }) + ", " + (imm).ToString());
		}
		insn_2:
		/* LD-rd-HL */
		if((insnBytes[0] & 0xC7) == 0x46) {
			var rd = (byte) ((byte) (insnBytes[0] >> 3) & 0x7);
			if(((uint) (((rd) != (0x6U)) ? 1U : 0U)) == 0)
				goto insn_3;
			pc += 1;
			return (string) ("ld " + (string) (rd switch { (byte) (0x0U) => "B", (byte) (0x1U) => "C", (byte) (0x2U) => "D", (byte) (0x3U) => "E", (byte) (0x4U) => "H", (byte) (0x5U) => "L", (byte) (0x7U) => "A", _ => throw new NotImplementedException() }) + ", (HL)");
		}
		insn_3:
		/* LD-HL-rs */
		if((insnBytes[0] & 0xF8) == 0x70) {
			var rs = (byte) ((byte) (insnBytes[0] >> 0) & 0x7);
			if(((uint) (((rs) != (0x6U)) ? 1U : 0U)) == 0)
				goto insn_4;
			pc += 1;
			return (string) ("ld (HL), " + (string) (rs switch { (byte) (0x0U) => "B", (byte) (0x1U) => "C", (byte) (0x2U) => "D", (byte) (0x3U) => "E", (byte) (0x4U) => "H", (byte) (0x5U) => "L", (byte) (0x7U) => "A", _ => throw new NotImplementedException() }));
		}
		insn_4:
		/* LD-HL-imm8 */
		if((insnBytes[0] & 0xFF) == 0x36) {
			var imm = (byte) ((byte) (insnBytes[1] >> 0) & 0xFF);
			pc += 2;
			return (string) ("ld (HL), " + (imm).ToString());
		}
		insn_5:
		/* LD-A-BC */
		if((insnBytes[0] & 0xFF) == 0xA) {
			pc += 1;
			return "ld A, (BC)";
		}
		insn_6:
		/* LD-A-DE */
		if((insnBytes[0] & 0xFF) == 0x1A) {
			pc += 1;
			return "ld A, (DE)";
		}
		insn_7:
		/* LD-BC-A */
		if((insnBytes[0] & 0xFF) == 0x2) {
			pc += 1;
			return "ld (BC), A";
		}
		insn_8:
		/* LD-DE-A */
		if((insnBytes[0] & 0xFF) == 0x12) {
			pc += 1;
			return "ld (DE), A";
		}
		insn_9:
		/* LD-A-imm16 */
		if((insnBytes[0] & 0xFF) == 0xFA) {
			var lsb = (byte) ((byte) (insnBytes[1] >> 0) & 0xFF);
			var msb = (byte) ((byte) (insnBytes[2] >> 0) & 0xFF);
			var addr = (ushort) ((((ushort) ((ushort) (((ushort) ((ushort) (msb))) << (int) (0x8U)))) | ((ushort) (lsb))));
			pc += 3;
			return (string) ("ld A, (" + (addr).ToString() + ")");
		}
		insn_10:
		/* LD-imm16-A */
		if((insnBytes[0] & 0xFF) == 0xEA) {
			var lsb = (byte) ((byte) (insnBytes[1] >> 0) & 0xFF);
			var msb = (byte) ((byte) (insnBytes[2] >> 0) & 0xFF);
			var addr = (ushort) ((((ushort) ((ushort) (((ushort) ((ushort) (msb))) << (int) (0x8U)))) | ((ushort) (lsb))));
			pc += 3;
			return (string) ("ld (" + (addr).ToString() + "), A");
		}
		insn_11:
		/* LDH-A-C */
		if((insnBytes[0] & 0xFF) == 0xF2) {
			pc += 1;
			return "ldh A, (C)";
		}
		insn_12:
		/* LDH-C-A */
		if((insnBytes[0] & 0xFF) == 0xE2) {
			pc += 1;
			return "ldh (C), A";
		}
		insn_13:
		/* LDH-A-imm8 */
		if((insnBytes[0] & 0xFF) == 0xF0) {
			var imm = (byte) ((byte) (insnBytes[1] >> 0) & 0xFF);
			var addr = (ushort) ((((ushort) ((ushort) ((ushort) (0xFF00U)))) | ((ushort) (imm))));
			pc += 2;
			return (string) ("ldh A, (" + (addr).ToString() + ")");
		}
		insn_14:
		/* LDH-imm8-A */
		if((insnBytes[0] & 0xFF) == 0xE0) {
			var imm = (byte) ((byte) (insnBytes[1] >> 0) & 0xFF);
			var addr = (ushort) ((((ushort) ((ushort) ((ushort) (0xFF00U)))) | ((ushort) (imm))));
			pc += 2;
			return (string) ("ldh (" + (addr).ToString() + "), A");
		}
		insn_15:
		/* LD-A-HL- */
		if((insnBytes[0] & 0xFF) == 0x3A) {
			pc += 1;
			return "ld A, (HL-)";
		}
		insn_16:
		/* LD-HL--A */
		if((insnBytes[0] & 0xFF) == 0x32) {
			pc += 1;
			return "ld (HL-), A";
		}
		insn_17:
		/* LD-A-HL+ */
		if((insnBytes[0] & 0xFF) == 0x2A) {
			pc += 1;
			return "ld A, (HL+)";
		}
		insn_18:
		/* LD-HL+-A */
		if((insnBytes[0] & 0xFF) == 0x22) {
			pc += 1;
			return "ld (HL+), A";
		}
		insn_19:
		/* LD-rr-imm16 */
		if((insnBytes[0] & 0xCF) == 0x1) {
			var r = (byte) ((byte) (insnBytes[0] >> 4) & 0x3);
			var lsb = (byte) ((byte) (insnBytes[1] >> 0) & 0xFF);
			var msb = (byte) ((byte) (insnBytes[2] >> 0) & 0xFF);
			var imm = (ushort) ((((ushort) ((ushort) (((ushort) ((ushort) (msb))) << (int) (0x8U)))) | ((ushort) (lsb))));
			pc += 3;
			return (string) ("ld " + (string) (r switch { (byte) (0x0U) => "BC", (byte) (0x1U) => "DE", (byte) (0x2U) => "HL", _ => "SP" }) + ", " + (imm).ToString());
		}
		insn_20:
		/* LD-imm16-SP */
		if((insnBytes[0] & 0xFF) == 0x8) {
			var lsb = (byte) ((byte) (insnBytes[1] >> 0) & 0xFF);
			var msb = (byte) ((byte) (insnBytes[2] >> 0) & 0xFF);
			var addr = (ushort) ((((ushort) ((ushort) (((ushort) ((ushort) (msb))) << (int) (0x8U)))) | ((ushort) (lsb))));
			pc += 3;
			return (string) ("ld (" + (addr).ToString() + "), SP");
		}
		insn_21:
		/* LD-SP-HL */
		if((insnBytes[0] & 0xFF) == 0xF9) {
			pc += 1;
			return "ld SP, HL";
		}
		insn_22:
		/* PUSH-rr */
		if((insnBytes[0] & 0xCF) == 0xC5) {
			var r = (byte) ((byte) (insnBytes[0] >> 4) & 0x3);
			pc += 1;
			return (string) ("push " + (string) (((uint) (((r) == (0x3U)) ? 1U : 0U) != 0) ? ("AF") : ((string) (r switch { (byte) (0x0U) => "BC", (byte) (0x1U) => "DE", (byte) (0x2U) => "HL", _ => "SP" }))));
		}
		insn_23:
		/* POP-rr */
		if((insnBytes[0] & 0xCF) == 0xC1) {
			var r = (byte) ((byte) (insnBytes[0] >> 4) & 0x3);
			pc += 1;
			return (string) ("pop " + (string) (((uint) (((r) == (0x3U)) ? 1U : 0U) != 0) ? ("AF") : ((string) (r switch { (byte) (0x0U) => "BC", (byte) (0x1U) => "DE", (byte) (0x2U) => "HL", _ => "SP" }))));
		}
		insn_24:
		/* JP-nn */
		if((insnBytes[0] & 0xFF) == 0xC3) {
			var lsb = (byte) ((byte) (insnBytes[1] >> 0) & 0xFF);
			var msb = (byte) ((byte) (insnBytes[2] >> 0) & 0xFF);
			var addr = (ushort) ((((ushort) ((ushort) (((ushort) ((ushort) (msb))) << (int) (0x8U)))) | ((ushort) (lsb))));
			pc += 3;
			return (string) ("jp " + (addr).ToString());
		}
		insn_25:
		/* JP-HL */
		if((insnBytes[0] & 0xFF) == 0xE9) {
			pc += 1;
			return "jp HL";
		}
		insn_26:
		/* JP-cc-imm16 */
		if((insnBytes[0] & 0xE7) == 0xC2) {
			var cc = (byte) ((byte) (insnBytes[0] >> 3) & 0x3);
			var lsb = (byte) ((byte) (insnBytes[1] >> 0) & 0xFF);
			var msb = (byte) ((byte) (insnBytes[2] >> 0) & 0xFF);
			var addr = (ushort) ((((ushort) ((ushort) (((ushort) ((ushort) (msb))) << (int) (0x8U)))) | ((ushort) (lsb))));
			pc += 3;
			return (string) ("jp " + (string) (cc switch { (byte) (0x0U) => "NZ", (byte) (0x1U) => "Z", (byte) (0x2U) => "NC", _ => "C" }) + ", " + (addr).ToString());
		}
		insn_27:
		/* JR-simm8 */
		if((insnBytes[0] & 0xFF) == 0x18) {
			var e = (byte) ((byte) (insnBytes[1] >> 0) & 0xFF);
			var offset = (sbyte) ((sbyte) (e));
			pc += 2;
			return (string) ("jr " + (offset).ToString());
		}
		insn_28:

        return null;
    }

    public static string ClassifyInstruction(Span<byte> insnBytes) {
		if((insnBytes[0] & 0xC0) == 0x40) {
			var rd = (byte) ((byte) (insnBytes[0] >> 3) & 0x7);
			var rs = (byte) ((byte) (insnBytes[0] >> 0) & 0x7);
			if(((uint) (((rs) != (0x6U)) ? 1U : 0U)) == 0)
				goto insn_1;
			return "LD-rd-rs";
		}
		insn_1:
		if((insnBytes[0] & 0xC7) == 0x6) {
			var rd = (byte) ((byte) (insnBytes[0] >> 3) & 0x7);
			var imm = (byte) ((byte) (insnBytes[1] >> 0) & 0xFF);
			if(((uint) (((rd) != (0x6U)) ? 1U : 0U)) == 0)
				goto insn_2;
			return "LD-rd-imm8";
		}
		insn_2:
		if((insnBytes[0] & 0xC7) == 0x46) {
			var rd = (byte) ((byte) (insnBytes[0] >> 3) & 0x7);
			if(((uint) (((rd) != (0x6U)) ? 1U : 0U)) == 0)
				goto insn_3;
			return "LD-rd-HL";
		}
		insn_3:
		if((insnBytes[0] & 0xF8) == 0x70) {
			var rs = (byte) ((byte) (insnBytes[0] >> 0) & 0x7);
			if(((uint) (((rs) != (0x6U)) ? 1U : 0U)) == 0)
				goto insn_4;
			return "LD-HL-rs";
		}
		insn_4:
		if((insnBytes[0] & 0xFF) == 0x36) {
			var imm = (byte) ((byte) (insnBytes[1] >> 0) & 0xFF);
			return "LD-HL-imm8";
		}
		insn_5:
		if((insnBytes[0] & 0xFF) == 0xA) {
			return "LD-A-BC";
		}
		insn_6:
		if((insnBytes[0] & 0xFF) == 0x1A) {
			return "LD-A-DE";
		}
		insn_7:
		if((insnBytes[0] & 0xFF) == 0x2) {
			return "LD-BC-A";
		}
		insn_8:
		if((insnBytes[0] & 0xFF) == 0x12) {
			return "LD-DE-A";
		}
		insn_9:
		if((insnBytes[0] & 0xFF) == 0xFA) {
			var lsb = (byte) ((byte) (insnBytes[1] >> 0) & 0xFF);
			var msb = (byte) ((byte) (insnBytes[2] >> 0) & 0xFF);
			var addr = (ushort) ((((ushort) ((ushort) (((ushort) ((ushort) (msb))) << (int) (0x8U)))) | ((ushort) (lsb))));
			return "LD-A-imm16";
		}
		insn_10:
		if((insnBytes[0] & 0xFF) == 0xEA) {
			var lsb = (byte) ((byte) (insnBytes[1] >> 0) & 0xFF);
			var msb = (byte) ((byte) (insnBytes[2] >> 0) & 0xFF);
			var addr = (ushort) ((((ushort) ((ushort) (((ushort) ((ushort) (msb))) << (int) (0x8U)))) | ((ushort) (lsb))));
			return "LD-imm16-A";
		}
		insn_11:
		if((insnBytes[0] & 0xFF) == 0xF2) {
			return "LDH-A-C";
		}
		insn_12:
		if((insnBytes[0] & 0xFF) == 0xE2) {
			return "LDH-C-A";
		}
		insn_13:
		if((insnBytes[0] & 0xFF) == 0xF0) {
			var imm = (byte) ((byte) (insnBytes[1] >> 0) & 0xFF);
			var addr = (ushort) ((((ushort) ((ushort) ((ushort) (0xFF00U)))) | ((ushort) (imm))));
			return "LDH-A-imm8";
		}
		insn_14:
		if((insnBytes[0] & 0xFF) == 0xE0) {
			var imm = (byte) ((byte) (insnBytes[1] >> 0) & 0xFF);
			var addr = (ushort) ((((ushort) ((ushort) ((ushort) (0xFF00U)))) | ((ushort) (imm))));
			return "LDH-imm8-A";
		}
		insn_15:
		if((insnBytes[0] & 0xFF) == 0x3A) {
			return "LD-A-HL-";
		}
		insn_16:
		if((insnBytes[0] & 0xFF) == 0x32) {
			return "LD-HL--A";
		}
		insn_17:
		if((insnBytes[0] & 0xFF) == 0x2A) {
			return "LD-A-HL+";
		}
		insn_18:
		if((insnBytes[0] & 0xFF) == 0x22) {
			return "LD-HL+-A";
		}
		insn_19:
		if((insnBytes[0] & 0xCF) == 0x1) {
			var r = (byte) ((byte) (insnBytes[0] >> 4) & 0x3);
			var lsb = (byte) ((byte) (insnBytes[1] >> 0) & 0xFF);
			var msb = (byte) ((byte) (insnBytes[2] >> 0) & 0xFF);
			var imm = (ushort) ((((ushort) ((ushort) (((ushort) ((ushort) (msb))) << (int) (0x8U)))) | ((ushort) (lsb))));
			return "LD-rr-imm16";
		}
		insn_20:
		if((insnBytes[0] & 0xFF) == 0x8) {
			var lsb = (byte) ((byte) (insnBytes[1] >> 0) & 0xFF);
			var msb = (byte) ((byte) (insnBytes[2] >> 0) & 0xFF);
			var addr = (ushort) ((((ushort) ((ushort) (((ushort) ((ushort) (msb))) << (int) (0x8U)))) | ((ushort) (lsb))));
			return "LD-imm16-SP";
		}
		insn_21:
		if((insnBytes[0] & 0xFF) == 0xF9) {
			return "LD-SP-HL";
		}
		insn_22:
		if((insnBytes[0] & 0xCF) == 0xC5) {
			var r = (byte) ((byte) (insnBytes[0] >> 4) & 0x3);
			return "PUSH-rr";
		}
		insn_23:
		if((insnBytes[0] & 0xCF) == 0xC1) {
			var r = (byte) ((byte) (insnBytes[0] >> 4) & 0x3);
			return "POP-rr";
		}
		insn_24:
		if((insnBytes[0] & 0xFF) == 0xC3) {
			var lsb = (byte) ((byte) (insnBytes[1] >> 0) & 0xFF);
			var msb = (byte) ((byte) (insnBytes[2] >> 0) & 0xFF);
			var addr = (ushort) ((((ushort) ((ushort) (((ushort) ((ushort) (msb))) << (int) (0x8U)))) | ((ushort) (lsb))));
			return "JP-nn";
		}
		insn_25:
		if((insnBytes[0] & 0xFF) == 0xE9) {
			return "JP-HL";
		}
		insn_26:
		if((insnBytes[0] & 0xE7) == 0xC2) {
			var cc = (byte) ((byte) (insnBytes[0] >> 3) & 0x3);
			var lsb = (byte) ((byte) (insnBytes[1] >> 0) & 0xFF);
			var msb = (byte) ((byte) (insnBytes[2] >> 0) & 0xFF);
			var addr = (ushort) ((((ushort) ((ushort) (((ushort) ((ushort) (msb))) << (int) (0x8U)))) | ((ushort) (lsb))));
			return "JP-cc-imm16";
		}
		insn_27:
		if((insnBytes[0] & 0xFF) == 0x18) {
			var e = (byte) ((byte) (insnBytes[1] >> 0) & 0xFF);
			var offset = (sbyte) ((sbyte) (e));
			return "JR-simm8";
		}
		insn_28:

        return null;
    }

    public const int InstructionCount = 28 + 0;
}
