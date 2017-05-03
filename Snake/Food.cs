using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Food
    {
        private int x, y, width, height;
        private SolidBrush brush;
        public Rectangle foodRec;

        public Food(Random randFood)
        {

            x = randFood.Next(0, 25) * 10;
            y = randFood.Next(0, 22) * 10;

            brush = new SolidBrush(Color.Black);

            // rozmiar jedzenia

            width = 10;
            height = 10;

            foodRec = new Rectangle(x, y, width, height);
        }

        public void foodLocation(Random randFood) // wylosowywuje lokacje
        {
            x = randFood.Next(0, 25) * 10;
            y = randFood.Next(0, 22) * 10;
        }

        public void drawFood(Graphics paper)
        {
            foodRec.X = x;
            foodRec.Y = y;

            paper.FillRectangle(brush, foodRec);
        } // rysowanie na już wylosowanej lokacji
    }
}
