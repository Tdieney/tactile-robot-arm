using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00_Test
{
    public enum eServoCommand : byte
    {
        Stop = 0, Up1, Down1, Up, Down
    };
    public class Servo
    {
        //command
        byte step = 1;
        byte delay = 50;
        eServoCommand command = eServoCommand.Stop;

        //setup
        byte downLimit = 70;
        byte upLimit = 80;
        bool isReverse = false;
        byte maxDelta = 15;

        public byte Step { get => step; set => step = value; }
        public byte Delay { get => delay; set => delay = value; }
        public eServoCommand Command { get => command; set => command = value; }
        public byte DownLimit { get => downLimit; set => downLimit = value; }
        public byte UpLimit { get => upLimit; set => upLimit = value; }
        public bool IsReverse { get => isReverse; set => isReverse = value; }
        public byte MaxDelta { get => maxDelta; set => maxDelta = value; }
        public bool IsRangeAvailable { get { return upLimit - downLimit >= MaxDelta ? true : false; } }

        public byte[] ArrayCommand
        {
            get => new byte[] { step, delay, (byte)command };
        }
        public byte[] ArraySetup
        {
            get => new byte[] { downLimit, upLimit, Convert.ToByte(isReverse) };
        }
        public byte[] ArrayWriteToFile
        {
            get => new byte[] { step, delay, downLimit, upLimit, Convert.ToByte(isReverse) };
        }
        public char[] ArrayCharCommand { get; set; }

        public Servo(char[] arrayCharCommand)
        {
            this.ArrayCharCommand = arrayCharCommand ?? throw new ArgumentNullException(nameof(arrayCharCommand)); ;
        }

        public void ChangeCommand(char key)
        {
            if (ArrayCharCommand == null)
                throw new ArgumentNullException(nameof(ArrayCharCommand));

            this.command = eServoCommand.Stop;

            for (int i = 0; i < ArrayCharCommand.Length; i++)
            {
                if (key == ArrayCharCommand[i])
                {
                    this.command = (eServoCommand)i;
                    break;
                }
            }
        }

        public eServoCommand FindCommand(char key)
        {
            for (int i = 0; i < ArrayCharCommand.Length; i++)
                if (key == ArrayCharCommand[i])
                    return (eServoCommand)i;

            return eServoCommand.Stop;
        }

        public void WriteToServo(byte[] arraySave)
        {
            if (arraySave == null)
                throw new ArgumentNullException(nameof(arraySave));

            step = arraySave[0];
            delay = arraySave[1];

            downLimit = arraySave[2];
            upLimit = arraySave[3];
            isReverse = Convert.ToBoolean(arraySave[4]);
        }
    }
}
