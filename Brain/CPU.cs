using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brain
{
	class CPU
	{
		/// <summary>
		/// main memory
		/// </summary>
		byte[] memory = new byte[8192];

		/// <summary>
		/// VRAM
		/// </summary>
		byte[] videoMemory = new byte[4096];

		/// <summary>
		/// stack pointer
		/// </summary>
		ushort stackPointer;

		/// <summary>
		/// memory pointer
		/// </summary>
		ushort memPointer;

		#region Statics
		ushort startUpPointer = 0x00;
        #endregion

        //registers
        byte a;
		byte b;
		ushort c;
		byte x;
		byte y;
		ushort z;

		List<byte> noParamOpcodes = new List<byte>()
		{
			{ 0x00 },
		};

		List<byte> singleParamOpcodes = new List<byte>()
		{
			{ 0x01 },
		};

		List<byte> doubleParamOpcodes = new List<byte>()
		{
			{ 0x07 },
		};



		public void StartUp()
        {
			memPointer = startUpPointer;

			for (; memPointer < 0xFF; memPointer++)
			{
				if (!OpExecuteErrorCheck(memory[memPointer]))
				{
					Console.WriteLine("Error!");
					memory[0x100] = 0x00;
				}
			}
			if (memory[memPointer] == 0x00)
            {
				ReturnError("Startup sequence failed!");
				Crash();
            }
        }

		void MainLoop()
		{
			byte op = memory[memPointer];
			if (noParamOpcodes.Contains(op))
			{
				OpExecute(memory[memPointer]);
			}
			else if (singleParamOpcodes.Contains(op))
            {
				ushort param1 = (byte)((byte)(memory[memPointer + 1]) | (byte)(memory[memPointer + 2]));
				OpExecute(memory[memPointer], param1);
            }
			else if (doubleParamOpcodes.Contains(op))
			{
				ushort param1 = (byte)((byte)(memory[memPointer + 1]) | (byte)(memory[memPointer + 2]));
				ushort param2 = (byte)((byte)(memory[memPointer + 3]) | (byte)(memory[memPointer + 4]));

				OpExecute(memory[memPointer], param1, param2);
			}
		}
	

		bool OpExecuteErrorCheck(byte _opcode)
        {
			return true;
        }

		void OpExecute(byte _opcode)
        {

        }

        void OpExecute(byte _opcode, ushort _param1)
        {
			switch (_opcode)
            {

				case 0x01:
					LDA(_param1);
					break;

				case 0x02:
					LDB(_param1);
					break;

				case 0x03:
					LDC(_param1);
					break;

				default:
					ReturnError($"No operation with opcode and {_opcode.ToString("X4")} and one argument!");
					break;
            }
        }

		void OpExecute(byte _opcode, ushort _param1, ushort _param2)
        {

        }


        #region Opcodes

		void NoOp()
        {

        }

		void LDA(ushort _location)
        {
			a = memory[_location];
        }

		void LDB(ushort _location)
        {
			b = memory[_location];
        }

		void LDC(ushort _location)
        {
			c = (ushort)(memory[_location] | memory[_location + 1]);
        }
		
		void LDX(ushort _location)
        {
			x = memory[_location];
        }

		void LDY(ushort _location)
        {
			y = memory[_location];
        }

		void LDZ(ushort _location)
        {
			z = (ushort)(memory[_location] | memory[_location + 1]);
        }
		#endregion

		void ReturnError(string error)
        {
			Console.WriteLine(error);
        }

		void Crash() { }
	}
}
