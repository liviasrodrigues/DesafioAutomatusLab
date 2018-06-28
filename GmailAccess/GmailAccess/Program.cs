using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace GmailAccess
{
    class Program
    {

        static void Main(string[] args)
        {
            //Instanciando o driver
            var driver = new ChromeDriver();

            //Acessando URL do Gmail
            driver.Navigate().GoToUrl("http://www.gmail.com");          
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            //Quando o campo email for visivel, entro com o email informado
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("identifierId")));
            Console.WriteLine("Informe seu Email:");
            var Email = Console.ReadLine();
                   
            var loginBox = driver.FindElement(By.Id("identifierId"));
            loginBox.SendKeys(Email);
            loginBox.SendKeys(Keys.Enter);

            //Quando o campo senha for visivel, entro com o a senha informada
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("password")));
            Console.WriteLine("Informe sua Senha:");
            var Password = Console.ReadLine();        

            var pwBox = driver.FindElement(By.Name("password"));
            pwBox.SendKeys(Password);
            pwBox.SendKeys(Keys.Enter);

            //Quando a tela for carregada, pego todos os emails não lidos da Caixa de Entrada
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.xT>div.y6>span>b")));
            List<IWebElement> Emails = driver.FindElements(By.CssSelector("div.xT>div.y6>span>b")).ToList();

            var countEmails = Emails.Count();

            //Se houver email não lido, abro o primeiro email
            if (countEmails != 0)
            {
               if (countEmails > 1)
                    Console.WriteLine("Você possui " + countEmails + " novos emails");
                else
                    Console.WriteLine("Você possui " + countEmails + " novos emails");


                Emails[0].Click();

            } else
            {
                Console.WriteLine("Você não possui novos E-mails");
            }

                          

            //var EmailContent = driver.FindElements(By.CssSelector("#\3a kt > div:nth-child(1)")); 
            Console.WriteLine("Fim");

         

        }

    }
}
