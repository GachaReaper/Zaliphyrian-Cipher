using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Code
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool encrypt = true;
            Console.WriteLine("Welcome to the Zaliphyrian Cipher. Would you like to encrypt a message, or decrypt an image? (Type 'encrypt' or 'decrypt'. Default is encrypt.");
            string ja = Console.ReadLine();
            if (ja == "decrypt")
            {
                encrypt = false;
            }
            else
            {
                encrypt = true;
            }

            if (encrypt == true)
            {
                Console.WriteLine("What would you like to encrypt? Default is Cheese.");
                string toEncode = Console.ReadLine();
                if (toEncode == "")
                {
                    toEncode = "Cheese";
                }
                byte[] bytes = Encoding.UTF8.GetBytes(toEncode);
                string bitString = string.Join(" ", bytes.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));
                int gridSize = (int)Math.Ceiling(Math.Sqrt(bitString.Length));

                int[,] gridLayout = new int[(int)Math.Pow(gridSize, 2), 2];

                int x = gridSize - 1;
                int y = gridSize - 1;

                //Get Grid Layout
                for (int i = 0; i < Math.Pow(gridSize, 2); i++)
                {
                    if (i == 0)
                    {
                        gridLayout[i, 0] = x;
                        gridLayout[i, 1] = y;
                    }

                    else if (Math.Round(Math.Sqrt(i + 1)) < Math.Sqrt(i + 1) & Math.Sqrt(i) % 1 == 0)
                    {
                        if (Math.Ceiling(Math.Sqrt(i + 1)) % 2 == 0)
                        {
                            x--;
                            gridLayout[i, 0] = x;
                            gridLayout[i, 1] = y;
                        }
                        else if (Math.Ceiling(Math.Sqrt(i + 1)) % 2 == 1)
                        {
                            y--;
                            gridLayout[i, 0] = x;
                            gridLayout[i, 1] = y;
                        }
                    }
                    else if (Math.Round(Math.Sqrt(i + 1)) < Math.Sqrt(i + 1) & Math.Sqrt(i) % 1 != 0)
                    {
                        if (Math.Ceiling(Math.Sqrt(i + 1)) % 2 == 0)
                        {
                            y--;
                            gridLayout[i, 0] = x;
                            gridLayout[i, 1] = y;
                        }
                        else if (Math.Ceiling(Math.Sqrt(i + 1)) % 2 == 1)
                        {
                            x--;
                            gridLayout[i, 0] = x;
                            gridLayout[i, 1] = y;
                        }
                    }
                    else if (Math.Round(Math.Sqrt(i + 1)) > Math.Sqrt(i + 1) & Math.Round(Math.Sqrt(i)) < Math.Sqrt(i))
                    {
                        if (Math.Ceiling(Math.Sqrt(i + 1)) % 2 == 0)
                        {
                            y--;
                            gridLayout[i, 0] = x;
                            gridLayout[i, 1] = y;
                        }
                        else if (Math.Ceiling(Math.Sqrt(i + 1)) % 2 == 1)
                        {
                            x--;
                            gridLayout[i, 0] = x;
                            gridLayout[i, 1] = y;
                        }
                    }
                    else if (Math.Round(Math.Sqrt(i + 1)) > Math.Sqrt(i + 1) & Math.Round(Math.Sqrt(i)) > Math.Sqrt(i) & Math.Sqrt(i + 1) % 1 != 0)
                    {
                        if (Math.Ceiling(Math.Sqrt(i + 1)) % 2 == 0)
                        {
                            x++;
                            gridLayout[i, 0] = x;
                            gridLayout[i, 1] = y;

                        }
                        else if (Math.Ceiling(Math.Sqrt(i + 1)) % 2 == 1)
                        {
                            y++;
                            gridLayout[i, 0] = x;
                            gridLayout[i, 1] = y;
                        }
                    }
                    else if (Math.Sqrt(i + 1) % 1 == 0)
                    {
                        if (Math.Ceiling(Math.Sqrt(i + 1)) % 2 == 0)
                        {
                            x++;
                            gridLayout[i, 0] = x;
                            gridLayout[i, 1] = y;
                        }
                        else if (Math.Ceiling(Math.Sqrt(i + 1)) % 2 == 1)
                        {
                            y++;
                            gridLayout[i, 0] = x;
                            gridLayout[i, 1] = y;
                        }
                    }
                }

                //create image

                Bitmap pic = new Bitmap(gridSize, gridSize, PixelFormat.Format16bppArgb1555);

                for (int i = 0; i < Math.Pow(gridSize, 2); i++)
                {
                    if (i == bitString.Length)
                    {
                        pic.SetPixel(gridLayout[i, 0], gridLayout[i, 1], Color.Red);
                    }
                    else if (i > bitString.Length)
                    {
                        pic.SetPixel(gridLayout[i, 0], gridLayout[i, 1], Color.Transparent);
                    }
                    else if (bitString[i] == '0')
                    {
                        pic.SetPixel(gridLayout[i, 0], gridLayout[i, 1], Color.White);
                    }
                    else if (bitString[i] == '1')
                    {
                        pic.SetPixel(gridLayout[i, 0], gridLayout[i, 1], Color.Black);
                    }
                    else if (bitString[i] == ' ')
                    {
                        pic.SetPixel(gridLayout[i, 0], gridLayout[i, 1], Color.Gray);
                    }

                }

                //save image

                Console.WriteLine("What would you like to name your image?");
                string name = Console.ReadLine();
                if (name == "")
                {
                    name = "Cheese";
                }

                pic.Save(Application.StartupPath + $"\\{name}.png", ImageFormat.Png);
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }

            else
            {
                Console.WriteLine("Place the Image you want to decrypt in this program's directory. Once you are done, press any key.");
                Console.ReadLine();

                Console.WriteLine("What is the name of your image? Don't include the file extension.");
                string name = Console.ReadLine();

                //create bitmap

                Bitmap bmp = new Bitmap(Application.StartupPath + $"\\{name}.png");

                int gridSize = bmp.Height;

                int[,] gridLayout = new int[(int)Math.Pow(gridSize, 2), 2];

                int x = gridSize - 1;
                int y = gridSize - 1;

                //Get Grid Layout
                for (int i = 0; i < Math.Pow(gridSize, 2); i++)
                {
                    if (i == 0)
                    {
                        gridLayout[i, 0] = x;
                        gridLayout[i, 1] = y;
                    }

                    else if (Math.Round(Math.Sqrt(i + 1)) < Math.Sqrt(i + 1) & Math.Sqrt(i) % 1 == 0)
                    {
                        if (Math.Ceiling(Math.Sqrt(i + 1)) % 2 == 0)
                        {
                            x--;
                            gridLayout[i, 0] = x;
                            gridLayout[i, 1] = y;
                        }
                        else if (Math.Ceiling(Math.Sqrt(i + 1)) % 2 == 1)
                        {
                            y--;
                            gridLayout[i, 0] = x;
                            gridLayout[i, 1] = y;
                        }
                    }
                    else if (Math.Round(Math.Sqrt(i + 1)) < Math.Sqrt(i + 1) & Math.Sqrt(i) % 1 != 0)
                    {
                        if (Math.Ceiling(Math.Sqrt(i + 1)) % 2 == 0)
                        {
                            y--;
                            gridLayout[i, 0] = x;
                            gridLayout[i, 1] = y;
                        }
                        else if (Math.Ceiling(Math.Sqrt(i + 1)) % 2 == 1)
                        {
                            x--;
                            gridLayout[i, 0] = x;
                            gridLayout[i, 1] = y;
                        }
                    }
                    else if (Math.Round(Math.Sqrt(i + 1)) > Math.Sqrt(i + 1) & Math.Round(Math.Sqrt(i)) < Math.Sqrt(i))
                    {
                        if (Math.Ceiling(Math.Sqrt(i + 1)) % 2 == 0)
                        {
                            y--;
                            gridLayout[i, 0] = x;
                            gridLayout[i, 1] = y;
                        }
                        else if (Math.Ceiling(Math.Sqrt(i + 1)) % 2 == 1)
                        {
                            x--;
                            gridLayout[i, 0] = x;
                            gridLayout[i, 1] = y;
                        }
                    }
                    else if (Math.Round(Math.Sqrt(i + 1)) > Math.Sqrt(i + 1) & Math.Round(Math.Sqrt(i)) > Math.Sqrt(i) & Math.Sqrt(i + 1) % 1 != 0)
                    {
                        if (Math.Ceiling(Math.Sqrt(i + 1)) % 2 == 0)
                        {
                            x++;
                            gridLayout[i, 0] = x;
                            gridLayout[i, 1] = y;

                        }
                        else if (Math.Ceiling(Math.Sqrt(i + 1)) % 2 == 1)
                        {
                            y++;
                            gridLayout[i, 0] = x;
                            gridLayout[i, 1] = y;
                        }
                    }
                    else if (Math.Sqrt(i + 1) % 1 == 0)
                    {
                        if (Math.Ceiling(Math.Sqrt(i + 1)) % 2 == 0)
                        {
                            x++;
                            gridLayout[i, 0] = x;
                            gridLayout[i, 1] = y;
                        }
                        else if (Math.Ceiling(Math.Sqrt(i + 1)) % 2 == 1)
                        {
                            y++;
                            gridLayout[i, 0] = x;
                            gridLayout[i, 1] = y;
                        }
                    }
                }

                int pixelCount = 0;

                for (int i = 0; i < Math.Pow(gridSize, 2); i++)
                {
                    Color pixelColor = bmp.GetPixel(gridLayout[i, 0], gridLayout[i, 1]);
                    if (pixelColor.A != 0)
                    {
                        pixelCount++;
                    }
                }

                string bitString = "";
                for (int i = 0; i < pixelCount - 1; i++)
                {
                    Color pixelColor = bmp.GetPixel(gridLayout[i, 0], gridLayout[i, 1]);
                    if (pixelColor.R == 255)
                    {
                        bitString += "0";
                    }
                    else if (pixelColor.R == 0)
                    {
                        bitString += "1";
                    }
                    else if (pixelColor.R == 132)
                    {
                        bitString += " ";
                    }
                }


                int numOfBytes = pixelCount / 9;
                byte[] bytes = new byte[numOfBytes];
                for (int i = 0; i < numOfBytes; ++i)
                {
                    bytes[i] = Convert.ToByte(bitString.Substring(9 * i, 8), 2);
                }

                string message = Encoding.UTF8.GetString(bytes);

                Console.WriteLine($"This image says '{message}'. Press any key to exit.");
                Console.ReadKey();
            }
        }
    }
}
