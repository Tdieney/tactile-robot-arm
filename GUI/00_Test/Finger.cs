using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00_Test
{
    public enum eFingerCommand : byte
    {
        Stop = 0, UpBot1Step, DownBot1Step, UpTop1Step, DownTop1Step,
        UpBotTop1Step, DownBotTop1Step, UpBotTop, DownBotTop
    }

    public class Finger
    {
        public static byte Head = 0xAA;
        public static byte Tail = 0x55;

        Servo bot;
        Servo top;
        eFingerCommand command = eFingerCommand.Stop;

        public byte[] ArrayCommand
        {
            get
            {
                byte[] cmd = new byte[] { (byte)command };
                byte[] botArr = bot.ArrayCommand;
                byte[] topArr = top.ArrayCommand;

                return cmd.Concat(botArr).ToArray().Concat(topArr).ToArray();
            }
        }
        public byte[] ArraySetup
        {
            get
            {
                byte[] cmd = new byte[] { (byte)eFingerCommand.Stop };
                byte[] botArr = bot.ArraySetup;
                byte[] topArr = top.ArraySetup;

                return cmd.Concat(botArr).ToArray().Concat(topArr).ToArray();
            }
        }
        public char[] ArrayCharCommand { get; set; }

        public Finger(Servo bot, Servo top, char[] arrayCharCommand)
        {
            this.bot = bot ?? throw new ArgumentNullException(nameof(bot));
            this.top = top ?? throw new ArgumentNullException(nameof(top));
            this.ArrayCharCommand = arrayCharCommand ?? throw new ArgumentNullException(nameof(arrayCharCommand)); ;
        }

        public void ChangeCommand(char key)
        {
            if (ArrayCharCommand == null)
                throw new ArgumentNullException(nameof(ArrayCharCommand));

            for (int i = 0; i < ArrayCharCommand.Length; i++)
            {
                if (key == ArrayCharCommand[i])
                {
                    this.command = (eFingerCommand)i;
                    break;
                }
            }
        }

        public void WriteToFinger(byte[] arraySave, int offset)
        {
            if (arraySave == null)
                throw new ArgumentNullException(nameof(arraySave));

        }
    }
}
