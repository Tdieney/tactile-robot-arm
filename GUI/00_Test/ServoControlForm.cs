using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _00_Test
{
    public partial class ServoControlForm : Form
    {
        MainForm motherForm;

        static byte Head = 0xAA;
        static byte Tail = 0x55;

        public AppendLogCallback AppendLog;

        string fileToSave = "test.bin";

        Servo bot1, top1, bot2, top2, bot3, top3;

        //ushort[] levelValue = new ushort[] { 0, 900, 1000, 1100, 1200, 1300, 1400, 1500, 1600, 1700, 1800 };

        public ServoControlForm(Form motherForm)
        {
            InitializeComponent();

            this.motherForm = motherForm as MainForm;

            this.AppendLog = this.motherForm.AppendLog;

            bot1 = new Servo(new char[] { 'Q', 'w', 'e' });
            top1 = new Servo(new char[] { 'Q', 'r', 't' });
            bot2 = new Servo(new char[] { 'Q', 's', 'd' });
            top2 = new Servo(new char[] { 'Q', 'f', 'g' });
            bot3 = new Servo(new char[] { 'Q', 'x', 'c' });
            top3 = new Servo(new char[] { 'Q', 'v', 'b' });
        }


        public void SaveByteArrayToFileWithFileStream(byte[] data, string filePath)
        {
            using (var stream = File.Create(filePath))
            {
                stream.Write(data, 0, data.Length);
            }
        }

        public byte[] ReadByteArrayFromFileWithFileStream(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }

        private void FingerControlForm_Load(object sender, EventArgs e)
        {
            try
            {
                byte[] data = ReadByteArrayFromFileWithFileStream(fileToSave);
                byte[] buf = new byte[5];

                if (data.Length == 30)
                {
                    Array.Copy(data, 0, buf, 0, buf.Length);
                    bot1.WriteToServo(buf);
                    Array.Copy(data, 5, buf, 0, buf.Length);
                    top1.WriteToServo(buf);

                    Array.Copy(data, 10, buf, 0, buf.Length);
                    bot2.WriteToServo(buf);
                    Array.Copy(data, 15, buf, 0, buf.Length);
                    top2.WriteToServo(buf);

                    Array.Copy(data, 20, buf, 0, buf.Length);
                    bot3.WriteToServo(buf);
                    Array.Copy(data, 25, buf, 0, buf.Length);
                    top3.WriteToServo(buf);


                    numStepBot1.Value = bot1.Step;
                    numStepBot2.Value = bot2.Step;
                    numStepBot3.Value = bot3.Step;
                    numStepTop1.Value = top1.Step;
                    numStepTop2.Value = top2.Step;
                    numStepTop3.Value = top3.Step;

                    numDelayBot1.Value = bot1.Delay;
                    numDelayBot2.Value = bot2.Delay;
                    numDelayBot3.Value = bot3.Delay;
                    numDelayTop1.Value = top1.Delay;
                    numDelayTop2.Value = top2.Delay;
                    numDelayTop3.Value = top3.Delay;

                    numUpLimitBot1.Value = bot1.UpLimit;
                    numUpLimitBot2.Value = bot2.UpLimit;
                    numUpLimitBot3.Value = bot3.UpLimit;
                    numUpLimitTop1.Value = top1.UpLimit;
                    numUpLimitTop2.Value = top2.UpLimit;
                    numUpLimitTop3.Value = top3.UpLimit;

                    numDownLimitBot1.Value = bot1.DownLimit;
                    numDownLimitBot2.Value = bot2.DownLimit;
                    numDownLimitBot3.Value = bot3.DownLimit;
                    numDownLimitTop1.Value = top1.DownLimit;
                    numDownLimitTop2.Value = top2.DownLimit;
                    numDownLimitTop3.Value = top3.DownLimit;

                    chReverseBot1.Checked = Convert.ToBoolean(bot1.IsReverse);
                    chReverseBot2.Checked = Convert.ToBoolean(bot2.IsReverse);
                    chReverseBot3.Checked = Convert.ToBoolean(bot3.IsReverse);
                    chReverseTop1.Checked = Convert.ToBoolean(top1.IsReverse);
                    chReverseTop2.Checked = Convert.ToBoolean(top2.IsReverse);
                    chReverseTop3.Checked = Convert.ToBoolean(top3.IsReverse);

                    chSync.Checked = true;
                }
            }
            catch (Exception ex)
            {
                AppendLog?.Invoke("Message from FingerControlForm: " + nameof(FingerControlForm_Load));
                AppendLog?.Invoke(ex.Message);
            }
        }

        private void chSync_CheckedChanged(object sender, EventArgs e)
        {
            if (chSync.Checked == true)
            {
                numDelayBot1.Enabled = false;
                numDelayTop1.Enabled = false;
                numDelayBot2.Enabled = false;
                numDelayBot3.Enabled = false;
                numDelayTop2.Enabled = false;
                numDelayTop3.Enabled = false;
            }
            else
            {
                numDelayBot1.Enabled = true;
                numDelayTop1.Enabled = true;
                numDelayBot2.Enabled = true;
                numDelayBot3.Enabled = true;
                numDelayTop2.Enabled = true;
                numDelayTop3.Enabled = true;
            }
        }

        private void FingerControlForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                bot1.Step = (byte)numStepBot1.Value;
                bot2.Step = (byte)numStepBot2.Value;
                bot3.Step = (byte)numStepBot3.Value;
                top1.Step = (byte)numStepTop1.Value;
                top2.Step = (byte)numStepTop2.Value;
                top3.Step = (byte)numStepTop3.Value;

                bot1.Delay = (byte)numDelayBot1.Value;
                bot2.Delay = (byte)numDelayBot2.Value;
                bot3.Delay = (byte)numDelayBot3.Value;
                top1.Delay = (byte)numDelayTop1.Value;
                top2.Delay = (byte)numDelayTop2.Value;
                top3.Delay = (byte)numDelayTop3.Value;

                bot1.DownLimit = (byte)numDownLimitBot1.Value;
                bot2.DownLimit = (byte)numDownLimitBot2.Value;
                bot3.DownLimit = (byte)numDownLimitBot3.Value;
                top1.DownLimit = (byte)numDownLimitTop1.Value;
                top2.DownLimit = (byte)numDownLimitTop2.Value;
                top3.DownLimit = (byte)numDownLimitTop3.Value;

                bot1.UpLimit = (byte)numUpLimitBot1.Value;
                bot2.UpLimit = (byte)numUpLimitBot2.Value;
                bot3.UpLimit = (byte)numUpLimitBot3.Value;
                top1.UpLimit = (byte)numUpLimitTop1.Value;
                top2.UpLimit = (byte)numUpLimitTop2.Value;
                top3.UpLimit = (byte)numUpLimitTop3.Value;

                bot1.IsReverse = Convert.ToBoolean(chReverseBot1.Checked);
                bot2.IsReverse = Convert.ToBoolean(chReverseBot2.Checked);
                bot3.IsReverse = Convert.ToBoolean(chReverseBot3.Checked);
                top1.IsReverse = Convert.ToBoolean(chReverseTop1.Checked);
                top2.IsReverse = Convert.ToBoolean(chReverseTop2.Checked);
                top3.IsReverse = Convert.ToBoolean(chReverseTop3.Checked);


                byte[] arr1 = bot1.ArrayWriteToFile.Concat(top1.ArrayWriteToFile).ToArray();
                byte[] arr2 = bot2.ArrayWriteToFile.Concat(top2.ArrayWriteToFile).ToArray();
                byte[] arr3 = bot3.ArrayWriteToFile.Concat(top3.ArrayWriteToFile).ToArray();
                byte[] arr123 = arr1.Concat(arr2).ToArray().Concat(arr3).ToArray();

                SaveByteArrayToFileWithFileStream(arr123, fileToSave);
            }
            catch (Exception ex)
            {
                AppendLog?.Invoke("Message from FingerControlForm: " + nameof(FingerControlForm_FormClosing));
                AppendLog?.Invoke(ex.Message);
            }
        }

        private void txtSendCommand_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (motherForm.port1.IsOpen)
                {
                    bot1.Command = eServoCommand.Stop;
                    bot2.Command = eServoCommand.Stop;
                    bot3.Command = eServoCommand.Stop;
                    top1.Command = eServoCommand.Stop;
                    top2.Command = eServoCommand.Stop;
                    top3.Command = eServoCommand.Stop;

                    switch (e.KeyChar)
                    {
                        // all
                        case 'Q':
                        case 'A':
                        case 'Z':
                        case 'Y':
                            bot1.Command = eServoCommand.Stop;
                            bot2.Command = eServoCommand.Stop;
                            bot3.Command = eServoCommand.Stop;
                            top1.Command = eServoCommand.Stop;
                            top2.Command = eServoCommand.Stop;
                            top3.Command = eServoCommand.Stop;
                            break;
                        case 'U':
                            bot1.Command = eServoCommand.Up;
                            bot2.Command = eServoCommand.Up;
                            bot3.Command = eServoCommand.Up;
                            top1.Command = eServoCommand.Up;
                            top2.Command = eServoCommand.Up;
                            top3.Command = eServoCommand.Up;
                            break;
                        case 'I':
                            bot1.Command = eServoCommand.Down;
                            bot2.Command = eServoCommand.Down;
                            bot3.Command = eServoCommand.Down;
                            top1.Command = eServoCommand.Down;
                            top2.Command = eServoCommand.Down;
                            top3.Command = eServoCommand.Down;
                            break;

                        // ALL top
                        case 'H':
                            top1.Command = eServoCommand.Up1;
                            top2.Command = eServoCommand.Up1;
                            top3.Command = eServoCommand.Up1;
                            break;
                        case 'J':
                            top1.Command = eServoCommand.Down1;
                            top2.Command = eServoCommand.Down1;
                            top3.Command = eServoCommand.Down1;
                            break;
                        case 'K':
                            top1.Command = eServoCommand.Up;
                            top2.Command = eServoCommand.Up;
                            top3.Command = eServoCommand.Up;
                            break;
                        case 'L':
                            top1.Command = eServoCommand.Down;
                            top2.Command = eServoCommand.Down;
                            top3.Command = eServoCommand.Down;
                            break;

                        // ALL bot
                        case 'N':
                            bot1.Command = eServoCommand.Up1;
                            bot2.Command = eServoCommand.Up1;
                            bot3.Command = eServoCommand.Up1;
                            break;
                        case 'M':
                            bot1.Command = eServoCommand.Down1;
                            bot2.Command = eServoCommand.Down1;
                            bot3.Command = eServoCommand.Down1;
                            break;
                        case ',':
                            bot1.Command = eServoCommand.Up;
                            bot2.Command = eServoCommand.Up;
                            bot3.Command = eServoCommand.Up;
                            break;
                        case '.':
                            bot1.Command = eServoCommand.Down;
                            bot2.Command = eServoCommand.Down;
                            bot3.Command = eServoCommand.Down;
                            break;

                        //finger 1
                        case 'W':
                            bot1.Command = eServoCommand.Up1;
                            top1.Command = eServoCommand.Up1;
                            break;
                        case 'E':
                            bot1.Command = eServoCommand.Down1;
                            top1.Command = eServoCommand.Down1;
                            break;
                        case 'R':
                            bot1.Command = eServoCommand.Up;
                            top1.Command = eServoCommand.Up;
                            break;
                        case 'T':
                            bot1.Command = eServoCommand.Down;
                            top1.Command = eServoCommand.Down;
                            break;
                        //finger 2
                        case 'S':
                            bot2.Command = eServoCommand.Up1;
                            top2.Command = eServoCommand.Up1;
                            break;
                        case 'D':
                            bot2.Command = eServoCommand.Down1;
                            top2.Command = eServoCommand.Down1;
                            break;
                        case 'F':
                            bot2.Command = eServoCommand.Up;
                            top2.Command = eServoCommand.Up;
                            break;
                        case 'G':
                            bot2.Command = eServoCommand.Down;
                            top2.Command = eServoCommand.Down;
                            break;
                        //finger 3
                        case 'X':
                            bot3.Command = eServoCommand.Up1;
                            top3.Command = eServoCommand.Up1;
                            break;
                        case 'C':
                            bot3.Command = eServoCommand.Down1;
                            top3.Command = eServoCommand.Down1;
                            break;
                        case 'V':
                            bot3.Command = eServoCommand.Up;
                            top3.Command = eServoCommand.Up;
                            break;
                        case 'B':
                            bot3.Command = eServoCommand.Down;
                            top3.Command = eServoCommand.Down;
                            break;
                        default:
                            bot1.ChangeCommand(e.KeyChar);
                            bot2.ChangeCommand(e.KeyChar);
                            bot3.ChangeCommand(e.KeyChar);
                            top1.ChangeCommand(e.KeyChar);
                            top2.ChangeCommand(e.KeyChar);
                            top3.ChangeCommand(e.KeyChar);
                            break;
                    }

                    bot1.Step = (byte)numStepBot1.Value;
                    bot2.Step = (byte)numStepBot2.Value;
                    bot3.Step = (byte)numStepBot3.Value;
                    top1.Step = (byte)numStepTop1.Value;
                    top2.Step = (byte)numStepTop2.Value;
                    top3.Step = (byte)numStepTop3.Value;


                    if (txtSendCommand.Text.Length > 10)
                        txtSendCommand.Clear();

                    //change delay for servos
                    if (chSync.Checked == true)
                    {
                        if (!(bot1.IsRangeAvailable && top1.IsRangeAvailable
                            && bot2.IsRangeAvailable && top2.IsRangeAvailable
                            && bot3.IsRangeAvailable && top3.IsRangeAvailable))
                            throw new Exception("servo range is invalid: \"up - down >= " + bot1.MaxDelta + "\"");

                        int timeDelayBot = (int)numDelayBot.Value;
                        int timeDelayTop = (int)numDelayTop.Value;
                        //up down ALL command
                        if (e.KeyChar == 'U') //up all
                            timeDelayBot = timeDelayTop + 1000;
                        else if (e.KeyChar == 'I') //down all 
                            timeDelayTop = timeDelayBot + 1000;

                        bot1.Delay = (byte)(timeDelayBot * bot1.Step / (bot1.UpLimit - bot1.DownLimit));
                        bot2.Delay = (byte)(timeDelayBot * bot2.Step / (bot2.UpLimit - bot2.DownLimit));
                        bot3.Delay = (byte)(timeDelayBot * bot3.Step / (bot3.UpLimit - bot3.DownLimit));
                        top1.Delay = (byte)(timeDelayTop * top1.Step / (top1.UpLimit - top1.DownLimit));
                        top2.Delay = (byte)(timeDelayTop * top2.Step / (top2.UpLimit - top2.DownLimit));
                        top3.Delay = (byte)(timeDelayTop * top3.Step / (top3.UpLimit - top3.DownLimit));
                    }
                    else
                    {
                        bot1.Delay = (byte)numDelayBot1.Value;
                        bot2.Delay = (byte)numDelayBot2.Value;
                        bot3.Delay = (byte)numDelayBot3.Value;
                        top1.Delay = (byte)numDelayTop1.Value;
                        top2.Delay = (byte)numDelayTop2.Value;
                        top3.Delay = (byte)numDelayTop3.Value;
                    }

                    //
                    //if (top1.Command == eServoCommand.Up)
                    //{
                    //    //top1.Delay = (byte)(top1.Delay - (top1.Delay / 2));
                    //    top1.Step = (byte)(top1.Step + 2);
                    //}
                    //if (top2.Command == eServoCommand.Up)
                    //{
                    //    //top2.Delay = (byte)(top2.Delay - (top2.Delay / 2));
                    //    top2.Step = (byte)(top2.Step + 2);
                    //}
                    //if (top3.Command == eServoCommand.Up)
                    //{
                    //    //top3.Delay = (byte)(top3.Delay - (top3.Delay / 2));
                    //    top3.Step = (byte)(top3.Step + 2);
                    //}



                    //transmit to STM32
                    byte[] arr1 = bot1.ArrayCommand.Concat(top1.ArrayCommand).ToArray();
                    byte[] arr2 = bot2.ArrayCommand.Concat(top2.ArrayCommand).ToArray();
                    byte[] arr3 = bot3.ArrayCommand.Concat(top3.ArrayCommand).ToArray();
                    byte[] arr123 = arr1.Concat(arr2).ToArray().Concat(arr3).ToArray();
                    List<byte> lst = arr123.ToList();
                    lst.Add((byte)numLevel.Value);
                    // ==> transmit here:
                    SendArray(lst.ToArray(), 0xAA, 0x55);
                }
            }
            catch (Exception ex)
            {
                AppendLog?.Invoke("Message from FingerControlForm: " + nameof(txtSendCommand_KeyPress));
                AppendLog?.Invoke(ex.Message);
            }
        }

        private void FiltInt(ref int value, int min, int max)
        {
            if (value > max)
                value = max;
            else if (value < min)
                value = min;
        }
        private void btnSetLimit_Click(object sender, EventArgs e)
        {
            try
            {
                if (motherForm.port1.IsOpen)
                {
                    ////servo
                    bot1.DownLimit = (byte)numDownLimitBot1.Value;
                    bot2.DownLimit = (byte)numDownLimitBot2.Value;
                    bot3.DownLimit = (byte)numDownLimitBot3.Value;
                    top1.DownLimit = (byte)numDownLimitTop1.Value;
                    top2.DownLimit = (byte)numDownLimitTop2.Value;
                    top3.DownLimit = (byte)numDownLimitTop3.Value;

                    bot1.UpLimit = (byte)numUpLimitBot1.Value;
                    bot2.UpLimit = (byte)numUpLimitBot2.Value;
                    bot3.UpLimit = (byte)numUpLimitBot3.Value;
                    top1.UpLimit = (byte)numUpLimitTop1.Value;
                    top2.UpLimit = (byte)numUpLimitTop2.Value;
                    top3.UpLimit = (byte)numUpLimitTop3.Value;

                    bot1.IsReverse = Convert.ToBoolean(chReverseBot1.Checked);
                    bot2.IsReverse = Convert.ToBoolean(chReverseBot2.Checked);
                    bot3.IsReverse = Convert.ToBoolean(chReverseBot3.Checked);
                    top1.IsReverse = Convert.ToBoolean(chReverseTop1.Checked);
                    top2.IsReverse = Convert.ToBoolean(chReverseTop2.Checked);
                    top3.IsReverse = Convert.ToBoolean(chReverseTop3.Checked);

                    //create array
                    byte[] arr1 = bot1.ArraySetup.Concat(top1.ArraySetup).ToArray();
                    byte[] arr2 = bot2.ArraySetup.Concat(top2.ArraySetup).ToArray();
                    byte[] arr3 = bot3.ArraySetup.Concat(top3.ArraySetup).ToArray();
                    byte[] arr123 = arr1.Concat(arr2).ToArray().Concat(arr3).ToArray();

                    //send array
                    List<byte> lst = arr123.ToList();
                    lst.Add((byte)numLevel.Value);
                    // ==> transmit here:
                    SendArray(lst.ToArray(), 0x55, 0xAA);
                }
            }
            catch (Exception ex)
            {
                AppendLog?.Invoke("Message from FingerControlForm: " + nameof(btnSetLimit_Click));
                AppendLog?.Invoke(ex.Message);
            }
        }

        private void SendArray(byte[] data, byte head, byte tail)
        {
            try
            {
                if (motherForm.port1.IsOpen)
                {
                    byte[] arr = new byte[data.Length + 2];
                    arr[0] = head;
                    arr[arr.Length - 1] = tail;
                    Array.Copy(data, 0, arr, 1, data.Length);
                    motherForm.port1.Write(arr, 0, arr.Length);
                }
            }
            catch (Exception ex)
            {
                AppendLog?.Invoke("Message from FingerControlForm: " + nameof(SendArray));
                AppendLog?.Invoke(ex.Message);
            }
        }
    }
}
