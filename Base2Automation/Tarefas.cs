using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Base2Automation
{
  [TestClass]
  public class Tarefas
  {
    private IWebDriver driver;
    //private Uri appURL = new Uri("https://mantis-prova.base2.com.br");
    private Uri appURL = new Uri("http://localhost/mantis");
    private WebElement element;
    private string user = "administrator";
    private string pwd = "root";
    //private string user = "VANDER_EVARISTO";
    //private string pwd = "216132";
    private Helper helper = new Helper();

    #region Test Cases
    [TestMethod]
    [TestCategory("Chrome")]
    public void Deve_conseguir_Criar_Nova_Tarefa()
    {
      helper.login(user,pwd,appURL,driver);
      element = helper.elementClassName("fa-edit");
      element.Click();
      IWebElement categoria = helper.elementById("category_id");
      SelectElement selectCategoria = new SelectElement(categoria);
      selectCategoria.SelectByIndex(2);

      IWebElement frequencia = helper.elementById("reproducibility");
      SelectElement selectFrequencia = new SelectElement(frequencia);
      selectFrequencia.SelectByValue("10");

      IWebElement gravidade = helper.elementById("severity");
      SelectElement selectGravidade = new SelectElement(gravidade);
      selectGravidade.SelectByText("trivial");

      IWebElement prioridade = helper.elementById("priority");
      SelectElement selectPrioridade = new SelectElement(prioridade);
      selectPrioridade.SelectByText("urgente");

      element = helper.elementById("summary");
      element.Click();
      element.Clear();
      element.SendKeys("Tarefa para correção de bug");
      string resumo = "Tarefa para correção de bug";

      element = helper.elementById("description");
      element.Click();
      element.Clear();
      element.SendKeys("Descrição da Tarefa");

      element = helper.elementById("steps_to_reproduce");
      element.Click();
      element.Clear();
      element.SendKeys("1 - Passo1 \r\n 2 - Passo2 \r\n 3 - Passo3");

      element = helper.elementById("additional_info");
      element.Click();
      element.Clear();
      element.SendKeys("Informações Adicionais");

      IWebElement tag = helper.elementById("tag_select");
      SelectElement selectTag = new SelectElement(tag);
      selectTag.SelectByText("Desenvolvimento");

      element = helper.elementXPath("//*[@id='report_bug_form']/div/div[2]/div[2]/input");
      element.Click();

      element = helper.elementXPath("//*[@id='main-container']/div[2]/div[2]/div/div[1]/div/div[2]/div[2]/div/table/tbody/tr[10]/td");
      string text = element.GetAttribute("innerText");      
      Assert.IsTrue(text.Contains(resumo), "A tarefa Foi incluída com sucesso!");

    }

    [TestMethod]
    [TestCategory("Chrome")]
    public void Deve_abrir_visao_de_tarefas_do_usuario()
    {
      helper.login(user, pwd, appURL, driver);
      element = helper.elementClassName("fa-dashboard");
      element.Click();
      Assert.IsTrue(driver.Title.Contains("Minha Visão"), "A visão de Tarefas foi aberta com sucesso!");
    }

    [TestMethod]
    [TestCategory("Chrome")]
    public void Deve_conseguir_editar_uma_tarefa_apos_criar()
    {
      Deve_conseguir_Criar_Nova_Tarefa();
      element = helper.elementXPath("//tr//td[@class='bug-id']");
      string bugId = element.GetAttribute("innerText");
      element = helper.elementClassName("fa-dashboard");
      element.Click();

      if (helper.elementXPath($"//a[text()='{bugId}']").Text.Contains(bugId))
      {
        element = helper.elementXPath($"//a[text()='{bugId}']");
      }
      element.Click();
      element = helper.elementById("bugnote_text");
      Assert.IsTrue(element.Displayed, "A tarefa foi aberta para edição com sucesso!");
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
