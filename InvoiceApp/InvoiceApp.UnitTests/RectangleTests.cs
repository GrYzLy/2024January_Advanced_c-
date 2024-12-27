using InvoiceApp.WebApi.Services;
using _2dFigures;

namespace InvoiceApp.UnitTests
{
    //    - dlugosc boku a
    //- dlugosc boku b
    //- pole
    public class RectangleTests
    {
        [Fact]
        public void SideA_Return_Value()
        {   
            var rectangle = new Rectangle(1,4);

            Assert.Equal(1, rectangle.SideA);
        }

        [Fact]
        public void SideA_NotAboveZero()
        {
            var rectangle = new Rectangle(-1, 4);

            Assert.InRange(rectangle.SideA, -99999999, 0);
        }

        [Fact]
        public void SideB_Return_Value()
        {
            var rectangle = new Rectangle(1, 4);

            Assert.Equal(4, rectangle.SideB);
        }

        [Fact]
        public void Area_Return_Value() {
            var rectangle = new Rectangle(2,2);
            Assert.Equal(4, rectangle.Area);
        }

        [Fact]
        public void Area_Count()
        {
            var rectangle = new Rectangle(3, 3);
            Assert.Equal(9, rectangle.Area);
        }

        [Theory]
        [InlineData(3,5,16)]
        [InlineData(5, 1, 12)]
        [InlineData(6, 1, 12)]
        public void GetPerimeter_WithVariusInputs(int sideA, int sideB, int expectedValue)
        {
            var rectangle = new Rectangle(sideA, sideB);

            var permieter = rectangle.GetPerimeter();

            Assert.Equal(expectedValue, permieter);
        }
    }
}