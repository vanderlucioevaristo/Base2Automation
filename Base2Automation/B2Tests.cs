using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace Base2Automation
{
  [TestClass]
  public class Base2Tests
  {
    private IWebDriver driver;
    private string appURL;
    private WebElement element;
    private bool isVisible;
    private string user = "VANDER_EVARISTO";
    private string pwd = "216132";

    [TestMethod]
    [TestCategory("Chrome")]
    public void Site_deve_estar_on_line()
    {
      driver.Navigate().GoToUrl(appURL + "/login_page.php");
      element = (WebElement)driver.FindElement(By.Id("login-box"));
      isVisible = element.Displayed;
      Assert.IsTrue(isVisible,"O site está on line");
      TestCleanup();
      TestCleanup();
    }

    [TestMethod]
    [TestCategory("Chrome")]
    public void Login_bem_sucedido()
    {
      driver.Navigate().GoToUrl(appURL + "/login_page.php");
      element=(WebElement)driver.FindElement(By.Id("username"));
      element.Click();
      element.Clear();
      element.SendKeys(user);
      driver.FindElement(By.ClassName("btn-success")).Click();
      element = (WebElement)driver.FindElement(By.Id("password"));
      element.Click();
      element.Clear();
      element.SendKeys(pwd);      
      element = (WebElement)driver.FindElement(By.XPath("//*[@id='login-form']/fieldset/div[1]/label/span"));
      if (!element.Selected)
      {
        element.Click();
      }

      element = (WebElement)driver.FindElement(By.XPath("//*[@id='login-form']/fieldset/div[2]/label/span"));

      if (!element.Selected)
      {
        element.Click();
      }

      driver.FindElement(By.ClassName("btn-success")).Click();

      element = (WebElement)driver.FindElement(By.ClassName("fa-edit"));
      isVisible = element.Displayed;

      Assert.IsTrue(isVisible, "O login do usuário: " + user + " foi feito com sucesso!!");

      TestCleanup();
    }

    [TestInitialize()]
    public void TestSetup()
    {
      appURL = " http://mantis-prova.base2.com.br";
      driver = new ChromeDriver();
    }

    [TestCleanup()]
    public void TestCleanup()
    {
      driver.Quit();
    }
  }

}
