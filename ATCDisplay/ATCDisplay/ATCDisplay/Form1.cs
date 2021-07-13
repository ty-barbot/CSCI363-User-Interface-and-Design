using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATCDisplay
{
    public partial class ATCDisp : Form
    {
        private double angle;
        private Rectangle WinBorder;
        private Rectangle InfoPanel;
        private SolidBrush brush;
        private Pen penThick;
        private Pen penThickPink;
        private Pen penThinPink;
        private Pen penThinWhite;
        private Pen penThin;
        private Pen usePen;
        private int simMode;
        private Image bmp;
        Airplane[] planes;

        public ATCDisp()
        {
            InitializeComponent();
            simMode = 0;
            Random rand = new Random();
            string[] randDest = {"KDLH", "KMSO", "KMSP", "CYWG", "KDEN" };
            bmp = Properties.Resources.terrain;

            angle = 0;
            WinBorder = new Rectangle(1, 1, ClientSize.Width - 3, ClientSize.Height - 3);
            InfoPanel = new Rectangle(ClientSize.Width / 2, 0, ClientSize.Width / 2, ClientSize.Height);
            brush = new SolidBrush(Color.FromArgb(0, 0, 100));
            penThick = new Pen(Color.FromArgb(0, 150, 0), 3);
            penThickPink = new Pen(Color.FromArgb(255, 50, 100), 3);
            penThinPink = new Pen(Color.FromArgb(255, 50, 100));
            penThinWhite = new Pen(Color.White);
            penThin = new Pen(Color.FromArgb(0, 150, 0));
            planes = new Airplane[5];

            // Initialization of planes array
            for(int i = 0; i < 5; i++)
            {
                int diameter = ClientSize.Width / 2 - 50;
                int radius = diameter / 2;

                double rad = degToRad(rand.Next(0, 359));
                int posX = (int)((6.0/5.0) * radius * Math.Cos(rad));
                int posY = (int)((6.0/5.0) * radius * Math.Sin(rad));
                int posAlt = rand.Next(1, 20) * 1500;

                int vectX;
                int vectY;
                int speed;
                if (posAlt < 10000)
                {
                    speed = 200;
                } 
                else if(posAlt < 20000)
                {
                    speed = 250;
                } 
                else
                {
                    speed = 300;
                }

                rad = degToRad(rand.Next(0, 359));
                vectX = (int)(0.02 * speed * Math.Cos(rad));
                vectY = (int)(0.02 * speed * Math.Sin(rad));
                
                string dest = randDest[rand.Next(0, randDest.Length - 1)];
                string callSign = "" + (char)rand.Next(65, 90) + (char)rand.Next(65, 90) + rand.Next(100, 999);

                planes[i] = new Airplane(posX, posY, posAlt, vectX, vectY, speed, dest, callSign);
                //planes[i] = new Airplane();
            }
        }

        private void AnimTicker_Tick(object sender, EventArgs e)
        {
            AnimTicker.Interval = 100;
            Invalidate();
            
        }

        private void ATCDisp_Load(object sender, EventArgs e)
        {
            //MessageBox.Show("TODO", "TODO: FIX PLANE HEADING ANGLES", MessageBoxButtons.OK);
            // TODO: Figure out what the h*ck we can use this function for
        }

        private void ATCDisp_Paint(object sender, PaintEventArgs e)
        {
            // First thing is to clear the screen to draw the next frame
            // Then, we draw the information panel showing each plane's data.
            // Lastly, we draw the radar panel showing airspace and plane position
            e.Graphics.Clear(Color.FromArgb(0, 0, 80));
            e.Graphics.DrawImage(bmp, 0, 0, 960, 1080);

            int endLoop = 0;
            switch (simMode)
            {
                case 0:
                    endLoop = planes.Length;
                    break;
                case 1:
                case 2:
                    endLoop = 1;
                    break;
                case 3:
                case 4:
                    endLoop = 2;
                    break;
            }
            showAirSpace(e, endLoop);
            showInfoPanel(e, endLoop);


            // Draws borders around the window. Makes things look nice and neat
            e.Graphics.DrawRectangle(penThick, WinBorder);
            e.Graphics.DrawLine(penThick, ClientSize.Width / 2, 0, ClientSize.Width / 2, ClientSize.Height);
        }

        #region DrawInfoPanel
        private void showInfoPanel(PaintEventArgs e, int endLoop)
        {
            e.Graphics.FillRectangle(brush, InfoPanel);
            for (int i = 0; i < endLoop; i++)
            {
                planes[i].PrintStats(e, 17 * ClientSize.Width / 32, 20 + i * 70, simMode);
            }
        }
        #endregion

        #region DrawAirspace
        private void showAirSpace(PaintEventArgs e, int endLoop)
        {
            /* 
               Some of the important numbers we're using:
                 ~ diameter -> diameter of the airspace circle, half the size of the window, less 50 pixels, to give 25 pixels on either side
                 ~ alpha    -> a small change in the angle for create a dashed circle
                 ~ radius   -> half of the amplitude i.e. the radius of the outer circle
            */
            int diameter = ClientSize.Width / 2 - 50;
            double alpha = degToRad(5);
            int radius = diameter / 2;

            // Now, to make drawing the concentric circles where we want a bit easier, translate
            // the origin to a point roughly to the first quarter of the window from the left.
            
            e.Graphics.TranslateTransform(25 + radius, ClientSize.Height / 2);
            e.Graphics.DrawEllipse(penThin, new Rectangle(-radius, -radius, diameter, diameter));

            // Draws dashed concentric circles. Much easier to count to 360 Degrees than to 2*pi,
            // so we can just convert our angle to radians after getting the angle. There will be
            // 36 dashes per concentric circles => 360 / 10 = 36
            for (int i = 0; i < 360; i += 10)
            {
                double theta = degToRad(i);

                // It's much easier to draw each level of concentricity per dash
                for (int j = 1; j < 5; j++)
                {
                    float x = (float)(radius * (j / 5.0) * Math.Cos(theta));
                    float y = (float)(radius * (j / 5.0) * Math.Sin(theta));
                    float deltX = (float)(radius * (j / 5.0) * Math.Cos(theta + alpha));
                    float deltY = (float)(radius * (j / 5.0) * Math.Sin(theta + alpha));
                    e.Graphics.DrawLine(penThin, x, y, deltX, deltY);
                }
            }
            e.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(0, 150, 0)), -5, -5, 10, 10);
            
            // Draws a thick pink line, representing runway 35L at KGFK, struck through by a thin pink line the height of the window
            e.Graphics.DrawLine(penThickPink, -10, 10, -10, -10);
            e.Graphics.DrawLine(penThinPink, -10, -ClientSize.Height / 2 + 3, -10, ClientSize.Height / 2 + 3);

            // Draw and update the planes in the airspace on top of the display.
            // Depending on what the Simulation mode is, we may want to only work 
            // with fewer planes just to illustrate the point of the test case 

            for (int i = 0; i < endLoop; i++)
            {

                if (planes[i].highLighted)
                {
                    usePen = penThin;
                } else
                {
                    usePen = penThinWhite;
                }
                if (simMode == 4 && planes[0].posX == planes[1].posX && planes[0].posY == planes[1].posY)
                {
                    Point p = new Point(planes[0].posX, planes[0].posY);
                    e.Graphics.DrawEllipse(new Pen(Color.Red), new Rectangle(p.X - 10, p.Y - 10, 20, 20));
                    e.Graphics.DrawLine(new Pen(Color.Red), p.X + (float)(-10 / Math.Sqrt(2)), p.Y + (float)(-10 / Math.Sqrt(2)), p.X + (float)(10 / Math.Sqrt(2)), p.Y + (float)(10 / Math.Sqrt(2)));
                    e.Graphics.DrawLine(new Pen(Color.Red), p.X + (float)(10 / Math.Sqrt(2)), p.Y + (float)(-10 / Math.Sqrt(2)), p.X + (float)(-10 / Math.Sqrt(2)), p.Y + (float)(10 / Math.Sqrt(2)));
                    AnimTicker.Interval = 2000;
                    planes[0].setPosVector(-350, 50, 100, 1, 0, 150, "KMSP", " ");
                    planes[1].setPosVector(350, 50, 100, -1, 0, 150, "KMSO", " ");
                } else if(simMode == 4 && Math.Sqrt(Math.Pow(planes[0].posX, 2) + Math.Pow(planes[1].posX, 2)) < 50)
                {
                    usePen = new Pen(Color.DarkOrange);
                    e.Graphics.RotateTransform(-90);
                    planes[i].show(e, usePen, radius, simMode);
                    e.Graphics.RotateTransform(90);
                    planes[i].update();
                } else
                {
                    e.Graphics.RotateTransform(-90);
                    planes[i].show(e, usePen, radius, simMode);
                    e.Graphics.RotateTransform(90);
                    planes[i].update();
                }
            }

            // This draws a radar line to indicate direction of the radar dish on top of the control tower
            e.Graphics.DrawLine(penThin, 0, 0, (int)(radius * Math.Cos(angle)), (int)(radius * Math.Sin(angle)));
            angle += 0.01;

            // Translate the origin back to the top left corner of the window
            e.Graphics.TranslateTransform(-1 * (25 + radius), -1 * (ClientSize.Height / 2));
        }
        #endregion
        
        private void ATCDisp_Click(object sender, EventArgs e)
        {
            //if (AnimTicker.Enabled) AnimTicker.Enabled = false;
            //else AnimTicker.Enabled = true;
            //using (var bmp = new Bitmap(ClientSize.Width, ClientSize.Height))
            //{
            //    this.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
            //    bmp.Save(@"c:\Users\j.neis\Desktop\test.png");
            //}
            //e.Graphics.TranslateTransform(25 + (ClientSize.Width / 2 - 50) / 2, ClientSize.Height / 2);
            for (int i = 0; i < planes.Length; i++)
            {
                planes[i].highLighted = false;
                CtrlLabel.Visible = false;
                CtrlBox.Enabled = false;
                CtrlBox.Visible = false;
            }
            for(int i = 0; i < planes.Length; i++)
            {
                int curX = this.PointToClient(Cursor.Position).X - (25 + (ClientSize.Width / 2 - 50) / 2);
                int curY = this.PointToClient(Cursor.Position).Y - (ClientSize.Height / 2);
                bool clickCondX = curX >= planes[i].posX - 5 && curX <= planes[i].posX + 5;
                bool clickCondY = curY >= planes[i].posY - 5 && curY <= planes[i].posY + 5;
                if (clickCondX && clickCondY) {
                    planes[i].highLighted = true;
                    CtrlLabel.Visible = true;
                    CtrlBox.Enabled = true;
                    CtrlBox.Visible = true;
                }
            }
            
        }

        private void CtrlBox_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                string vect = CtrlBox.Text;
                char[] sep = { ',', ' ' };
                
                for (int i = 0; i < planes.Length; i++)
                {
                    if(planes[i].highLighted)
                    {
                        int newVectX = planes[i].vectX, newVectY = planes[i].vectY;
                        try
                        {
                            newVectX = Convert.ToInt32(vect.Split(sep)[0]);
                            newVectY = Convert.ToInt32(vect.Split(sep)[2]);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Something went wrong. Unable to change vector.\nProvided values: " + vect.Split(sep)[0] + ", " + vect.Split(sep)[2],
                                "Attn:", MessageBoxButtons.OK);
                            return;
                        }
                        planes[i].setVector(newVectX, newVectY);
                        break;
                    }
                }
                e.Handled = true;
            }
        }

        private double degToRad(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        #region buttonsSimMode
        private void BtnApproach_Click(object sender, EventArgs e)
        {
            simMode = 1;
            planes[0].setPosVector(250, 400, 26, (int)(0.01 * 150 * Math.Cos(-3 * Math.PI / 4)), (int)(0.01 * 150 * Math.Sin(-3 * Math.PI / 4)), 150, "KGFK", "A");
        }

        private void BtnDepart_Click(object sender, EventArgs e)
        {
            simMode = 2;
            planes[0].setPosVector(0, 0, 0, -1, 0, 20, "EDDB", "D");
        }

        private void BtnColAvoid_Click(object sender, EventArgs e)
        {
            simMode = 3;
        }

        private void BtnCollide_Click(object sender, EventArgs e)
        {
            simMode = 4;
            planes[0].setPosVector(-300, 50, 100, 1, 0, 150, "KMSP", " ");
            planes[1].setPosVector(300, 50, 100, -1, 0, 150, "KMSO", " ");
        }
        #endregion
    }
}
