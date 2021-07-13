using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ATCDisplay
{
    class Airplane
    {
        public int posX;
        public int posY;
        private int orgX;
        private int orgY;
        private int posAlt;
        private int orgAlt;
        public int vectX;
        private int orgVectX;
        public int vectY;
        private int orgVectY;
        private int speed;
        private string dest;
        private string callSign;
        private string ATCcontrol;
        public bool highLighted;
        private Random rand;

        public Airplane(int posX, int posY, int posAlt, int vectX, int vectY, int speed, string dest, string callSign)
        {
            highLighted = false;
            rand = new Random();
            this.posX     = posX;
            orgX = posX;
            this.posY     = posY;
            orgY = posY;
            this.posAlt   = posAlt;
            this.vectX    = vectX;
            this.vectY    = vectY;
            this.speed    = speed;
            this.dest     = dest;
            this.callSign = callSign;
            this.ATCcontrol = "A";
        }

        public Airplane() // This constructor needs reworking. Do not use.
        {
            string[] randDest = { "KDLH", "KMSO", "KMSP", "CYWG", "KDEN" };
            rand = new Random();

            double rad = degToRad(rand.Next(0, 359));
            this.posX = (int)(295 * Math.Cos(rad));
            this.posY = (int)(295 * Math.Sin(rad));
            this.posAlt = rand.Next(1, 20) * 1500;

            
            if (posAlt < 10000)
            {
                this.speed = 200;
            }
            else if (posAlt < 20000)
            {
                this.speed = 250;
            }
            else
            {
                this.speed = 300;
            }

            rad = degToRad(rand.Next(0, 359));
            this.vectX = (int)(0.02 * speed * Math.Cos(rad));
            this.vectY = (int)(0.02 * speed * Math.Sin(rad));

            this.dest = randDest[rand.Next(0, randDest.Length - 1)];
            this.callSign = "" + (char)rand.Next(65, 90) + (char)rand.Next(65, 90) + rand.Next(100, 999);
        }

        public void show(PaintEventArgs e, Pen p, int radius, int simMode)
        {
            
            bool showCondition = Math.Sqrt(Math.Pow(posX, 2) + Math.Pow(posY, 2)) <= (6.0 / 5.0) * radius;
            switch (simMode)
            {
                #region Normal
                case 0: // Normal flight case, no special action
                    if (showCondition)
                    {
                        //e.Graphics.DrawLine(p, posX - 5, posY - 5, posX + 5, posY + 5);
                        //e.Graphics.DrawLine(p, posX - 5, posY + 5, posX + 5, posY - 5);
                        
                        double deg = 0;
                        if (vectX != 0) deg = 180 * Math.Atan(vectY / vectX) / Math.PI;
                        else if (vectX == 0 && vectY < 0) deg = 0;
                        else if (vectX == 0 && vectY > 0) deg = 180;
                        else if (vectY == 0 && vectX < 0) deg = 270;
                        else if (vectY == 0 && vectX > 0) deg = 90;
                        
                        if (deg < 0) deg += 360;
                        e.Graphics.RotateTransform(90);
                        e.Graphics.FillEllipse(new SolidBrush(p.Color), new Rectangle(posX - 5, posY - 5, 10, 10));
                        e.Graphics.DrawString(callSign + " " + dest + " " + ATCcontrol + "\n" + posAlt + " " +
                                              speed + " " + (int)deg, new Font("Courier New", 8), new SolidBrush(p.Color), posX + 7, posY - 5);
                        e.Graphics.RotateTransform(-90);
                    }
                    else
                    {
                        posX = orgX;
                        posY = orgY;
                        double rad = Math.PI * rand.Next(0, 359) / 180.0;
                        vectX = (int)(0.02 * speed * Math.Cos(rad));
                        vectY = (int)(0.02 * speed * Math.Sin(rad));
                    }
                    break;
                #endregion
                #region Approach
                case 1: // Approach the runway
                    showCondition = posX != 0 || posY != 0;
                    if(showCondition)
                    {
                        if(posX == -10)
                        {
                            setVector(0, -1 * (int)(0.01 * speed));
                        }
                        if(posY < 130 && posY != 0)
                        {
                            posAlt = posY / 5;
                        }
                        if(posY == 0)
                        {
                            setVector(1, 0);
                        }
                        e.Graphics.RotateTransform(90);
                        e.Graphics.FillEllipse(new SolidBrush(p.Color), new Rectangle(posX - 5, posY - 5, 10, 10));
                        double deg = 0;
                        if(vectY != 0) deg = 180 * Math.Atan(vectX / vectY) / Math.PI;
                        e.Graphics.DrawString(callSign + " " + dest + " " + ATCcontrol + "\n" + posAlt + " " +
                                              speed + " " + (int)deg, new Font("Courier New", 8), new SolidBrush(p.Color), posX + 7, posY - 5);
                        e.Graphics.RotateTransform(-90);
                    } else
                    {
                        posX = orgX;
                        posY = orgY;
                        posAlt = orgAlt;
                        vectX = orgVectX;
                        vectY = orgVectY;
                    }
                    break;
                #endregion
                #region Departure
                case 2:
                    showCondition = Math.Sqrt(Math.Pow(posX, 2) + Math.Pow(posY, 2)) <= (6.0 / 5.0) * radius;
                    if (showCondition)
                    {
                        if(posX == -10)
                        {
                            vectX = 0;
                            vectY = -1;
                            speed = 150;
                        }
                        if(posY > -130 && posY != 0)
                        {
                            posAlt = -1 * posY / 3;
                        }
                        if(posY < -130)
                        {
                            vectX = -1;
                        }


                        e.Graphics.RotateTransform(90);
                        e.Graphics.FillEllipse(new SolidBrush(p.Color), new Rectangle(posX - 5, posY - 5, 10, 10));
                        double deg = 0;
                        if (vectY != 0) deg = 180 * Math.Atan(vectX / vectY) / Math.PI;
                        e.Graphics.DrawString(callSign + " " + dest + " " + ATCcontrol + "\n" + posAlt + " " +
                                              speed + " " + (int)deg, new Font("Courier New", 8), new SolidBrush(p.Color), posX + 7, posY - 5);
                        e.Graphics.RotateTransform(-90);
                    }
                    else
                    {
                        posX = orgX;
                        posY = orgY;
                        posAlt = orgAlt;
                        vectX = orgVectX;
                        vectY = orgVectY;
                    }
                    break;
                #endregion
                #region CollisionAvoidance
                case 3:
                    //double deg = 0;
                    e.Graphics.RotateTransform(90);
                    e.Graphics.FillEllipse(new SolidBrush(p.Color), new Rectangle(posX - 5, posY - 5, 10, 10));
                    e.Graphics.DrawString(callSign + " " + dest + " " + ATCcontrol + "\n" + posAlt + " " +
                                          speed + " 000", new Font("Courier New", 8), new SolidBrush(p.Color), posX + 7, posY - 5);
                    e.Graphics.RotateTransform(-90);
                    break;
                #endregion
                #region Collision
                case 4:
                    //double deg = 0;
                    e.Graphics.RotateTransform(90);
                    e.Graphics.FillEllipse(new SolidBrush(p.Color), new Rectangle(posX - 5, posY - 5, 10, 10));
                    e.Graphics.DrawString(callSign + " " + dest + " " + ATCcontrol + "\n" + posAlt + " " +
                                          speed + " 000", new Font("Courier New", 8), new SolidBrush(p.Color), posX + 7, posY - 5);
                    e.Graphics.RotateTransform(-90);
                    break;
                    #endregion
            }
        }

        public void update()
        {
            posX += vectX;
            posY += vectY;
        }

        public void PrintStats(PaintEventArgs e, int x, int y, int simMode) 
        {
            e.Graphics.DrawString(getInfo(), new Font("Courier New", 8), new SolidBrush(Color.FromArgb(0, 150, 0)), x, y);
            e.Graphics.DrawString("For debug. Sim Mode: " + simMode, new Font("Courier New", 8), new SolidBrush(Color.FromArgb(255, 0, 0)), 1096, 605);
        }

        public string getInfo()
        {
            if(posAlt >= 18000)
            {
                return callSign + "\n" +
                   "(x, y), alt: (" + posX + ", " + posY + "), FL" + posAlt / 100 + "\n" +
                   "<x, y>, speed: <" + vectX + ", " + vectY + ">, " + speed + "kts\n" +
                   "Destination: " + dest + "\n";
            }
            else if(posAlt < 10000)
            {
                return callSign + "\n" +
                   "(x, y), alt: (" + posX + ", " + posY + "), 0" + posAlt + "\n" +
                   "<x, y>, speed: <" + vectX + ", " + vectY + ">, " + speed + "kts\n" +
                   "Destination: " + dest + "\n";
            }
            else
            {
                return callSign + "\n" +
                   "(x, y, alt): (" + posX + ", " + posY + ", " + posAlt + ")\n" +
                   "<x, y>, speed: <" + vectX + ", " + vectY + ">, " + speed + "kts\n" +
                   "Destination: " + dest + "\n";
            }
            
        }
        private double degToRad(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public void setPosVector(int x, int y, int alt, int vectX, int vectY, int speed, string dest, string ATCcontrol)
        {
            this.speed = speed;
            this.posAlt = alt;
            this.orgAlt = alt;
            this.posX = x;
            this.orgX = x;
            this.posY = y;
            this.orgY = y;
            this.vectX = vectX;
            this.orgVectX = vectX;
            this.vectY = vectY;
            this.orgVectY = vectY;
            this.dest = dest;
            this.ATCcontrol = ATCcontrol;
        }

        public void setVector(int x, int y)
        {
            this.vectX = x;
            this.vectY = y;
        }

        public int getSpeed()
        {
            return speed;
        }
    }
}
