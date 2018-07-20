using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class SpecialtyTests : IDisposable
  {
    public void Dispose()
    {
      Specialty.DeleteAll();
    }
    public SpecialtyTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=hisato_kawaminami_test;";
    }
    [TestMethod]
      public void GetName_ReturnsName_String()
      {
        //Arrange
        string name = "Yoko Bono";
        Specialty newSpecialty = new Specialty(name);

        //Act
        string result = newSpecialty.GetName();
        // Console.WriteLine(result);
        //Assert
        Assert.AreEqual(name, result);
      }
      [TestMethod]
      public void SetName_SetName_String()
      {
        //Arrange
        string name = "Yoko Bono";


        Specialty newSpecialty = new Specialty(name);

        //Act
        string updatedName = "Jon Lemon";
        newSpecialty.SetName(updatedName);
        string result = newSpecialty.GetName();


      }
      [TestMethod]
      public void GetAll_DbStartsEmpty_0()
      {
        //Arrange
        //ACT
        int result = Specialty.GetAll().Count;

        //Assert
        Assert.AreEqual(0, result);
      }
      [TestMethod]
      public void Equals_ReturTrueIfNamesAreTheSame_Specialty()
      {
        //Arrange, Act
        Specialty firstSpecialty = new Specialty("Yoko Bono");
        Specialty secondSpecialty = new Specialty("Yoko Bono");


        //Assert
        Assert.AreEqual(firstSpecialty, secondSpecialty);
      }
      [TestMethod]
      public void Save_SavesToDatabase_SpecialtyList()
      {
        //Arrange
        Specialty testSpecialty = new Specialty("Yoko Bono");

        //Act
        testSpecialty.Save();
        List<Specialty> result = Specialty.GetAll();
        List<Specialty> testList = new List<Specialty>{testSpecialty};

        //Assert
        CollectionAssert.AreEqual(testList, result);
      }
      [TestMethod]
      public void Save_AssignsIdToObject_Id()
      {
        //Arrange
        Specialty testSpecialty = new Specialty("Yoko Bono");

        //Act
        testSpecialty.Save();
        Specialty savedSpecialty = Specialty.GetAll()[0];

        int result = savedSpecialty.GetId();
        int testId = testSpecialty.GetId();

        //Assert
        Assert.AreEqual(testId, result);
      }
      [TestMethod]
      public void Find_FindsItemInDatabase_Item()
      {
        //Arrange
        Specialty testSpecialty = new Specialty("Yoko Bono");
        testSpecialty.Save();

        //Act
        Specialty foundSpecialty = Specialty.Find(testSpecialty.GetId());

        //Assert
        Assert.AreEqual(testSpecialty,foundSpecialty);
      }

      [TestMethod]
      public void Delete_A_Specific_Specialty()
      {
        //Arrange
        Specialty testSpecialty = new Specialty("Yoko");
        testSpecialty.Save();
        Specialty testSpecialty2 = new Specialty("Jon");
        testSpecialty2.Save();

        //Act
        testSpecialty.Delete();
        List<Specialty> expectedList = new List<Specialty>{testSpecialty2};

        //Assert
        List<Specialty> outputList = Specialty.GetAll();
        Assert.IsTrue(outputList.Count ==1);
        CollectionAssert.AreEqual(expectedList, outputList);
      }
      [TestMethod]
      public void Edit_EditSpecialtyNameInDatabase_String()
      {
        //Arrange
        string testName = "Yoko";
        Specialty testSpecialty = new Specialty(testName);
        testSpecialty.Save();
        string editName = "John";

        //Act
        testSpecialty.Edit(editName);
        string result = Specialty.Find(testSpecialty.GetId()).GetName();

        //Assert
        Assert.AreEqual(editName, result);

      }
  }
}
