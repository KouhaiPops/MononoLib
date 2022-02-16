using Microsoft.Xna.Framework;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoTest.Chip
{
    public class Emulator : Base.IGameElement
    {
        private short fontCharacterStride = 5;
        private byte[] characters = new byte[]
        {
            //0b_1111_0000, // 0
            //0b_1001_0000,
            //0b_1001_0000,
            //0b_1001_0000,
            //0b_1111_0000,

            //0b_0011_0000, // 1
            //0b_1101_0000,
            //0b_0001_0000,
            //0b_0001_0000,
            //0b_0001_0000,


            //0b_1111_0000, // 2
            //0b_0001_0000,
            //0b_1111_0000,
            //0b_1000_0000,
            //0b_1111_0000,

            //0b_1111_0000, // 3
            //0b_0001_0000,
            //0b_1111_0000,
            //0b_0001_0000,
            //0b_1111_0000,

            //0b_0011_0000, // 4
            //0b_1101_0000,
            //0b_0001_0000,
            //0b_0001_0000,
            //0b_0001_0000,


            0b11110000
            ,0b10010000
            ,0b10010000
            ,0b10010000
            ,0b11110000


            ,0b00100000
            ,0b01100000
            ,0b00100000
            ,0b00100000
            ,0b01110000


            ,0b11110000
            ,0b00010000
            ,0b11110000
            ,0b10000000
            ,0b11110000


            ,0b11110000
            ,0b00010000
            ,0b11110000
            ,0b00010000
            ,0b11110000


            ,0b10010000
            ,0b10010000
            ,0b11110000
            ,0b00010000
            ,0b00010000


            ,0b11110000
            ,0b10000000
            ,0b11110000
            ,0b00010000
            ,0b11110000


            ,0b11110000
            ,0b10000000
            ,0b11110000
            ,0b10010000
            ,0b11110000


            ,0b11110000
            ,0b00010000
            ,0b00100000
            ,0b01000000
            ,0b01000000


            ,0b11110000
            ,0b10010000
            ,0b11110000
            ,0b10010000
            ,0b11110000


            ,0b11110000
            ,0b10010000
            ,0b11110000
            ,0b00010000
            ,0b11110000


            ,0b11110000
            ,0b10010000
            ,0b11110000
            ,0b10010000
            ,0b10010000
            
            ,0b11100000
            ,0b10010000
            ,0b11100000
            ,0b10010000
            ,0b11100000

            ,0b11110000
            ,0b10000000
            ,0b10000000
            ,0b10000000
            ,0b11110000


            ,0b11100000
            ,0b10010000
            ,0b10010000
            ,0b10010000
            ,0b11100000


            ,0b11110000
            ,0b10000000
            ,0b11110000
            ,0b10000000
            ,0b11110000


            ,0b11110000
            ,0b10000000
            ,0b11110000
            ,0b10000000
            ,0b10000000

        };
        private byte delayTimer = 0;
        private byte soundTimer = 0;

        private bool[] keys = new bool[16];
        private const short PROGRAM_INDEX = 512;
        public bool DEBUGGER_ATTACHED { get; set; } = false;
        private short opcode;
        private Random rng = new Random();
        private byte[] memory = new byte[4096];
        private byte[] generalRegister = new byte[16];
        private short instructionPointer = PROGRAM_INDEX;
        private short Pointer;
        private short programSize = 0;
        private Num addrRegister;

        internal Engine.ChipScene Scene { get; init; }
        public void Load(string path)
        {
            var programData = File.ReadAllBytes(path);
            programSize = (short)programData.Length;

            Array.Copy(programData, 0, memory, PROGRAM_INDEX, programSize);
            Array.Copy(characters, 0, memory,0, characters.Length);
        }

        //public void Emulate()
        //{
        //    if(DEBUGGER_ATTACHED)
        //    {
        //        DumpMemory();
        //    }
        //    while(true)
        //    {
        //        // Read next instruction
        //        ReadOp();

        //    }
        //}

        private void DumpMemory()
        {
            var relativeProgramSize = instructionPointer + programSize;
            while (instructionPointer < relativeProgramSize)
            {
                Read();
                System.Diagnostics.Debug.WriteLine($"OPCODE: {opcode:X4}");
            }
        }

        private void Read()
        {
            // Read op code
            opcode = (short)((memory[instructionPointer] << 8) + memory[instructionPointer + 1]);
            instructionPointer += 2;
        }

        private void ReadOp()
        {
            if(DEBUGGER_ATTACHED)
            {
                return;
            }
            // Read op code
            Read();

            // Decode op code
            switch(opcode & 0xF000)
            {
                case 0x0000:
                    switch(opcode & 0x0F00)
                    {
                        case 0x0000:
                            switch(opcode & 0x000F)
                            {
                                case 0x0000:
                                    ReturnFromSubroutine();
                                    break;
                                case 0x000E:
                                    ClearScreen();
                                    break;
                                default:
                                    panic("Unkown opcode");
                                    break;
                            }
                            break;
                        default:
                            CallMachinecode();
                            break;
                    }
                    break;
                case 0x1000:
                    HandleGoto();
                    break;
                case 0x2000:
                    CallSubroutine();
                    break;
                case 0x3000:
                    SkipIfRegisterEqualsValue();
                    break;
                case 0x4000:
                    SkipIfRegisterNotEqualVal();
                    break;
                case 0x5000:
                    SkipIfRegistersEquals();
                    break;
                case 0x6000:
                    MoveValueToRegister();
                    break;
                case 0x7000:
                    AddRnC();
                    break;
                case 0xA000:
                    SetPointer();
                    break;
                case 0xD000:
                    DrawAtPoint();
                    break;
                default:
                    break;

            }
            // Execute op code
        }

        #region Helper methods
        private short Rand()
        {
            return (byte)rng.Next(256);
        }
        private void panic(string message)
        {
            System.Diagnostics.Debug.WriteLine($"Message: {message}\nOpcode: {opcode} At {instructionPointer}\n");
        }

        private short GetAddressFromOpcode()
        {
            return (short)(opcode & 0xFFF);
        }

        // Layout: 0x0123
        // 
        private byte GetByteAtPosition(int position)
        {
            var pos = 4 * (3 - position);
            if(pos < 0)
            {
                return 0;
            }
            var clearVal = ((opcode & (0xF << pos)) >> pos);
            return (byte)clearVal;
        }

        private short GetNumberAtPosition(int start, int end = 3)
        {
            var width = (end - start)+1;
            if(width < 0)
            {
                return 0;
            }
            var offset = (int)Math.Pow(16, width)-1;
            return (short)(opcode & offset);

        }

        private (byte register, short val) GetRegisterAndVal()
        {
            return (GetByteAtPosition(1), GetNumberAtPosition(2));
        }

        private (byte r1, byte r2) GetRegisters()
        {
            return (GetByteAtPosition(1), GetByteAtPosition(2));
        }

        private bool IsPressed(byte keycode)
        {
            if(keycode <= 15)
            {
                return keys[keycode];
            }
            return false;
        }

        private short GetCharacter(byte character)
        {
            if(character > 0xF)
            {
                return 0;
            }
            return memory[character * fontCharacterStride];
        }
        #endregion

        #region Opcodes

        // 0x0 NNN
        private void CallMachinecode()
        {
            var addr = GetAddressFromOpcode();
            instructionPointer = addr;
        }

        // 0x00E0
        private void ClearScreen()
        {
            Scene.Clear();
        }

        // 0x00EE
        private void ReturnFromSubroutine()
        {

        }

        // 0X1 NNN
        private void HandleGoto()
        {
            var addr = GetAddressFromOpcode();
            instructionPointer = addr;
        }

        // 0x2 NNN
        private void CallSubroutine()
        {

        }

        // 0x3 X NN
        // Skip if equals
        private void SkipIfRegisterEqualsValue()
        {
            (byte register, short val) = GetRegisterAndVal();
            if(generalRegister[register] == val)
            {
                Read();
            }
        }

        // 0x4 X NN
        // Skip if not equals
        private void SkipIfRegisterNotEqualVal()
        {
            (byte register, short val) = GetRegisterAndVal();

            if(generalRegister[register] != val)
            {
                Read();
            }
        }

        // 0x5 X Y 0
        // Skip if registers  equal
        private void SkipIfRegistersEquals()
        {
            (byte r1, byte r2) = GetRegisters();
            if(generalRegister[r1] == generalRegister[r2])
            {
                Read();
            }
        }

        // 0x6 X NN
        // Move NN to X
        private void MoveValueToRegister()
        {
            var r = GetByteAtPosition(1);
            var value = GetNumberAtPosition(2);
            generalRegister[r] = (byte)value;
        }

        // 0x7 X NN
        // Add NN to X, no carry
        private void AddRnC()
        {
            var (register, val) = GetRegisterAndVal();
            generalRegister[register] += (byte)val;
        }

        // 0x8 X Y 0
        // Assign the value in Y to X
        private void AssignR()
        {
            var (r1, r2) = GetRegisters();
            generalRegister[r1] = generalRegister[r2];
        }

        // 0x8 X Y 1
        // Xor X by Y
        private void Or()
        {
            var (r1, r2) = GetRegisters();
            generalRegister[r1] |= generalRegister[r2];
        }

        // 0x8 X Y 2
        // And X by Y
        private void And()
        {
            var (r1, r2) = GetRegisters();
            generalRegister[r1] &= generalRegister[r2];
        }

        // 0x8 X Y 3
        // Xor X by Y
        private void Xor()
        {
            var (r1, r2) = GetRegisters();
            generalRegister[r1] ^= generalRegister[r2];
        }

        
        // 0x8 X Y 4
        // Add Y to X, and carry
        private void Add()
        {
            var (r1, r2) = GetRegisters();
            generalRegister[r1] += generalRegister[r2];
            if(generalRegister[r1] < generalRegister[r2])
            {
                generalRegister[15] = 1;
            }
        }

        // 0x8 X Y 5
        // Sub Y from X then set VF flag to 0 for borrow
        private void Sub()
        {
            var (r1, r2) = GetRegisters();
            var rTemp = generalRegister[r1] - generalRegister[r2];
            if(rTemp > generalRegister[r1])
            {
                generalRegister[15] = 0;
            }
            else
            {
                generalRegister[15] = 1;
            }
            generalRegister[r1] = (byte)rTemp;

        }

        // 0x8 X Y 6
        // Shift X to the right by 1, set carry flag to least bit?
        private void Sar()
        {
            var (r1, r2) = GetRegisters();
            generalRegister[15] |= (byte)(generalRegister[r1] & 1);
            generalRegister[r1] >>= 1;
        }


        // 0x8 X Y 7
        // Sub X from Y then set to X, set VF flag to 0 if borrow
        private void SubO()
        {

        }

        // 0x9 X Y 0
        // Skip if X doesn't equal Y
        private void SrneR()
        {
            var (r1, r2) = GetRegisters();
            if(generalRegister[r1] != generalRegister[r2])
            {
                Read();
            }
        }

        // 0xA NNN
        // Set I (instruction pointer) to NNN
        private void SetPointer()
        {
            var addr = GetAddressFromOpcode();
            Pointer = addr;
        }

        // 0xB NNN
        // Jump to address NNN plus register V0
        private void JmpPlus()
        {
            var addr = GetAddressFromOpcode();
            var v0 = generalRegister[0];
            instructionPointer = (short)(addr + v0);
        }

        // 0xC X NN
        // Set regsiter X to random value AND NN
        private void SetRand()
        {
            var (register, val) = GetRegisterAndVal();
            generalRegister[register] = (byte)(Rand() & val);
        }

        // 0xD X Y N
        // Draw a sprite at X and Y, with 8 pixel width and N height
        /*
         * Draws a sprite at coordinate (VX, VY) that has a width of 8 pixels
         * and a height of N pixels.
         * Each row of 8 pixels is read as bit-coded starting from memory location I;
         * I value does not change after the execution of this instruction.
         * As described above, 
         * VF is set to 1 if any screen pixels are flipped from set to unset when the sprite is drawn, 
         * and to 0 if that does not happen
         */
        public void DrawAtPoint()
        {
            var x = generalRegister[GetByteAtPosition(1)];
            var y = generalRegister[GetByteAtPosition(2)];
            var height = GetByteAtPosition(3);
            for(int i = 0; i < height; i++)
            {
                // Decode the bit-coded byte
                var b = memory[Pointer+i];
                for(int j = 0; j < 8; j++)
                {
                    var flip = b & (0b10000000 >> j);
                    if(flip != 0)
                    {
                        Scene.Draw(x + j, y + i);
                    }
                }
            }
        }


        // 0xE X 9 E
        // Skip next if key on X is pressed
        private void SkipIfKeyPressed()
        {
            var r = GetByteAtPosition(1);
            if(IsPressed(generalRegister[r]))
            {
                Read();
            }
        }

        // 0xE X A 1
        // Skip next if key isn't pressed
        private void SkipIfKeyNotPressed()
        {
            var r = GetByteAtPosition(1);
            if (!IsPressed(generalRegister[r]))
            {
                Read();
            }
        }

        // 0xF X 0 7
        // Set X to value of delay timer
        private void SetRegisterToDelay()
        {
            var r = GetByteAtPosition(1);
            generalRegister[r] = delayTimer;
        }

        // 0xF X 0 A
        // Blocking call, set X to the value of the get_key
        private void GetPress()
        {
            // TODO blocking call?
        }

        // 0xF X 15
        // Set delay timer to X
        private void SetDelayTimer()
        {
            var r = GetByteAtPosition(1);
            delayTimer = generalRegister[r];
        }

        // 0xF X 18
        // Set sound time to X
        private void SetSoundTimer()
        {
            var r = GetByteAtPosition(1);
            soundTimer = generalRegister[r];
        }

        // 0xF X 1E
        // Add X to I, not carry
        private void AddIns()
        {
            Pointer += generalRegister[GetByteAtPosition(1)];
        }

        // 0xF X 29
        // Sets I to the location of the sprite for the character in VX.
        // Characters 0-F (in hexadecimal) are represented by a 4x5 font.
        private void SetToSprite()
        {
            var r = GetByteAtPosition(1);
            var character = generalRegister[r];
            short characterMemLocation = GetCharacter(character);
            Pointer = characterMemLocation;
        }


        // 0xF X 33
        // Stores the binary-coded decimal representation of VX,
        // with the most significant of three digits at the address in I,
        // the middle digit at I plus 1, and the least significant digit at I plus 2.
        // (In other words, take the decimal representation of VX,
        // place the hundreds digit in memory at location in I,
        // the tens digit at location I+1, and the ones digit at location I+2.);
        private void SetBCD()
        {

        }

        // 0xF X 55
        // Stores from V0 to VX (including VX) in memory, starting at address I. The offset from I is increased by 1 for each value written,
        // but I itself is left unmodified.
        private void Store()
        {

        }

        // 0xF X 65
        // Fills from V0 to VX (including VX) with values from memory,
        // starting at address I.
        // The offset from I is increased by 1 for each value written,
        // but I itself is left unmodified
        private void Load()
        {

        }

        #endregion
        public void Update(GameTime gameTime)
        {
            Tick();
        }

        private void Tick()
        {
            if(delayTimer > 0)
            {
                delayTimer--;
            }
            if(soundTimer > 0)
            {
                soundTimer--;
            }
            ReadOp();
        }
    }
}
