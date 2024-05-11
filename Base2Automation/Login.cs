using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium;



namespace Base2Automation
{
  [TestClass]
  public class Login
  {
    private IWebDriver driver;
    private Uri appURL = new Uri("https://mantis-prova.base2.com.br");
    //private Uri appURL = new Uri("http://localhost/mantis");
    private WebElement element;
    private bool isVisible;
    //private string user = "administrator";
    //private string pwd = "root";
    private string user = "VANDER_EVARISTO";
    private string pwd = "216132";
    private Helper helper = new Helper();

    #region Test Cases

    [TestMethod]
    [TestCategory("Chrome")]
    public void Site_deve_estar_on_line()
    {
      driver.Navigate().GoToUrl(appURL + "/login_page.php");
      element = (WebElement)driver.FindElement(By.Id("login-box"));
      isVisible = element.Displayed;
      Assert.IsTrue(isVisible, "O site está on line");
      TestCleanup();
    }

    [TestMethod]
    [TestCategory("Chrome")]
    public void Login_deve_ser_bem_sucedido()
    {
      helper.login(user, pwd, appURL, driver);
      element = (WebElement)driver.FindElement(By.ClassName("fa-edit"));
      isVisible = element.Displayed;

      Assert.IsTrue(isVisible, "O login do usuário: " + user + " foi feito com sucesso!!");

      TestCleanup();
    }

    [TestMethod]
    [TestCategory("Chrome")]
    public void Não_deve_permitir_de_Login_com_usuario_inexistente()
    {
      helper.login("LOGIN_INEXISTENTE", pwd, appURL, driver);
      element = helper.elementByclassName("alert-danger");
      isVisible = element.Displayed;
      Assert.IsTrue(isVisible, "O teste de login com usuário inexistente foi bem sucedido!");
    }

    [TestMethod]
    [TestCategory("Chrome")]
    public void Não_deve_permitir_de_Login_com_senha_incorreta()
    {
      helper.login(user, "senhaincorreta", appURL, driver);
      element = helper.elementByclassName("alert-danger");
      isVisible = element.Displayed;
      Assert.IsTrue(isVisible, "O teste de login com usuário inexistente foi bem sucedido!");
    }

    [TestMethod]
    [TestCategory("Chrome")]
    public void Não_deve_permitir_de_Login_com_usuario_em_branco()
    {
      helper.login("", pwd, appURL, driver);
      element = helper.elementByclassName("alert-danger");
      isVisible = element.Displayed;
      Assert.IsTrue(isVisible, "O teste de login com usuário inexistente foi bem sucedido!");
    }

    [TestMethod]
    [TestCategory("Chrome")]
    public void Deve_consegir_fazer_logout_no_sistema()
    {
      helper.login(user, pwd, appURL, driver);
      element = helper.elementClassName("fa-angle-down");
      element.Click();
      element = helper.elementByclassName("fa-sign-out");
      element.Click();
    }

    [TestMethod]
    [TestCategory("Chrome")]
    public void Deve_conseguir_as_configuracoes_da_conta_e_alterar_a_senha()
    {
      string newPwd = "216132";
      helper.login(user, pwd, appURL, driver);
      element = helper.elementXPath("//span[@class='user-info']");
      element.Click();
      element = helper.elementCss("a[href='/account_page.php']");
      element.Click();
      element = helper.elementById("password-current");
      element.Click();
      element.Clear();
      element.SendKeys(pwd);

      element = helper.elementById("password");
      element.Click();
      element.Clear();
      element.SendKeys(newPwd);

      element = helper.elementById("password-confirm");
      element.Click();
      element.Clear();
      element.SendKeys(newPwd);

      element = helper.elementXPath("//input[@type='submit' and @value='Atualizar Usuário']");
      element.Click();

      element = helper.elementClassName("fa-angle-down");
      element.Click();
      element = helper.elementByclassName("fa-sign-out");
      element.Click();

      helper.login(user, newPwd, appURL, driver);
      string url = driver.Url;

      Assert.IsTrue(driver.Url==url, "O login do usuário: " + user + " foi feito com sucesso após alteração da senha!");


    }


    #endregion

    #region Environment

    [TestInitialize()]
    public void TestSetup()
    {
      driver = helper.createBrowser("Chrome");
    }

    [TestCleanup()]
    public void TestCleanup()
    {
      driver.Quit();
    }

    #endregion
  }

}
