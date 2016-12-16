using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Eating.Model;

namespace BoligTest2
{
    [TestClass]
    public class UnitTest1
    {

        private Bolig bolig = new Bolig();
        /*KuverterPerBolig test*/
        [TestMethod]
        public void TestMethod1()
        {
            bolig.NumberAdults = 0;
            bolig.NumberKidsFourSix = 0;
            bolig.NUmberKidsSevenFifteen = 0;

           double result =  bolig.kuverterPerBolig();

            Assert.AreEqual(0, result);
        }

        /*KuverterPerBolig test*/
        [TestMethod]
        public void TestMethod2()
        {
            bolig.NumberAdults = 2;
            bolig.NumberKidsFourSix = 0;
            bolig.NUmberKidsSevenFifteen = 2;

            double result = bolig.kuverterPerBolig();

            Assert.AreEqual(3, result);
        }

        /*KuverterPerBolig test*/
        [TestMethod]
        public void TestMethod3()
        {
            bolig.NumberAdults = 1;
            bolig.NumberKidsFourSix = 2;
            bolig.NUmberKidsSevenFifteen = 2;

            double result = bolig.kuverterPerBolig();

            Assert.AreEqual(2.5, result);
        }

        /*BeregnPris test*/
        [TestMethod]
        public void TestMethod4()
        {
            bolig.NumberAdults = 1;
            bolig.NumberKidsFourSix = 0;
            bolig.NUmberKidsSevenFifteen = 0;

            double PrisResultat = bolig.BeregnPris(500);
            Assert.AreEqual(500, PrisResultat);
        }
        /*BeregnPris test*/
        [TestMethod]
        public void TestMethod5()
        {
            bolig.NumberAdults = 2;
            bolig.NumberKidsFourSix = 0;
            bolig.NUmberKidsSevenFifteen = 1;

            double PrisResultat = bolig.BeregnPris(500);
            Assert.AreEqual(200, PrisResultat);
        }
        /*BeregnPris test*/
        [TestMethod]
        public void TestMethod6()
        {
            bolig.NumberAdults = 1;
            bolig.NumberKidsFourSix = 3;
            bolig.NUmberKidsSevenFifteen = 0;

            double PrisResultat = bolig.BeregnPris(500);
            Assert.AreEqual(285.714, PrisResultat,0.001);
        }
    }
}
