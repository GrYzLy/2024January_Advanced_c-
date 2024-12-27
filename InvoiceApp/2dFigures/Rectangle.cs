namespace _2dFigures
{
    public class Rectangle
    {
        public int SideA { get; set; }
        public int SideB { get; set; }

        public int Area
        {
            get
            { return SideA * SideB; }
        }

        public Rectangle(int sideA, int sideB) 
        {
            SideA = sideA;
            SideB = sideB;
        }

        public int GetPerimeter()
        {
            return 2 * SideA + 2 * SideB;                
        }


    }
}
