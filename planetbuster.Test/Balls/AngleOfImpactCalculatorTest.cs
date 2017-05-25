using Assets.Scripts.Balls;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using planetbuster.Test.TestUtil;
using UnityEngine;

namespace planetbuster.Test.Balls
{
    [TestFixture]
    public class AngleOfImpactCalculatorTest : TestBase
    {
        public static object[] AngleSources = new object[]
        {
            new object[] {0f, new Vector3(0,5,0), new Vector3(0,0,0)},
            new object[] {90f, new Vector3(5,0,0), new Vector3(0,0,0)},
            new object[] {270f, new Vector3(-5,0,0), new Vector3(0,0,0)},
            new object[] {180f, new Vector3(0,-5,0), new Vector3(0,0,0)},

            new object[] {45f, new Vector3(5,5,0), new Vector3(0,0,0)},
            new object[] {135f, new Vector3(5,-5,0), new Vector3(0,0,0)},
            new object[] {225f, new Vector3(-5,-5,0), new Vector3(0,0,0)},
            new object[] {315f, new Vector3(-5,5,0), new Vector3(0,0,0)},

            new object[] {322.352386f, new Vector3(-51,75,0), new Vector3(3,5,0)},
            new object[] {91.2730255f, new Vector3(53,1,0), new Vector3(8,2,0)},
            new object[] {223.331665f, new Vector3(-28,3,0), new Vector3(22,56,0)},
        };

        [Test]
        [TestCaseSource(nameof(AngleSources))]
        public void TestAngleOfImpact(float expectedAngle, Vector3 projectilePosition, Vector3 gridBallPosition)
        {
            var angleOfImpactCalculator = new AngleOfImpactCalculator();
            var angle = angleOfImpactCalculator.Calculate(projectilePosition, gridBallPosition);
            Assert.AreEqual(expectedAngle, angle);
        }
    }
}