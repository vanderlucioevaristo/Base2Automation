using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace Base2Automation
{
  public class Helper
  {
    private IWebDriver driver;
    private WebElement element;
    public IWebDriver createBrowser(string browser)
    {
      switch (browser)
      {
        case "Chrome":
          driver = new ChromeDriver();
          break;
        case "Firefox":
          driver = new FirefoxDriver();
          break;
        case "IE":
          driver = new InternetExplorerDriver();
          break;
        default:
          driver = new ChromeDriver();
          break;
      }
      return driver;
    }

    public void login(string user, string pwd, Uri appURL, IWebDriver driver)
    {
      bool emptyUser = user==""?true:false;
      driver.Navigate().GoToUrl(appURL + "/login_page.php");
      element = this.elementById("username");
      element.Click();
      element.Clear();
      element.SendKeys(user);
      driver.FindElement(By.ClassName("btn-success")).Click();
      if (!emptyUser)
      {
        element = this.elementById("password");
        element.Click();
        element.Clear();
        element.SendKeys(pwd);
        element = this.elementXPath("//*[@id='login-form']/fieldset/div[1]/label/span");
        if (!element.Selected)
        {
          element.Click();
        }

        element = this.elementXPath("//*[@id='login-form']/fieldset/div[2]/label/span");

        if (!element.Selected)
        {
          element.Click();
        }

      }


      driver.FindElement(By.ClassName("btn-success")).Click();
    }

    public WebElement elementByclassName(string element)
    {
      return (WebElement)driver.FindElement(By.ClassName(element));
    }

    public WebElement elementById(string element)
    {
      return (WebElement)driver.FindElement(By.Id(element));
    }

    public WebElement elementXPath(string element)
    {
      return (WebElement)driver.FindElement(By.XPath(element));
    }

    public WebElement elementClassName(string element)
    {
      return (WebElement)driver.FindElement(By.ClassName(element));
    }

    public WebElement elementCss(string element)
    {
      return (WebElement)driver.FindElement(By.CssSelector(element));
    }  
  }
}
