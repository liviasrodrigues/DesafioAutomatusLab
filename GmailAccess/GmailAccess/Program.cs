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
  
        public static ChromeDriver driver { set; get; } 
        public static WebDriverWait wait { set; get; } 

        static void Main(string[] args)
        { 
            //Instanciando o driver
            driver = new ChromeDriver();
            
            //Acessando URL do Gmail para HTML básico
            driver.Navigate().GoToUrl(" https://mail.google.com/mail/?ui=html&zy=h");          
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            Console.WriteLine();

            //Efetuando Login
            Console.WriteLine("Informe seu Email:");
            var Email = Console.ReadLine();
            Console.WriteLine("Informe sua Senha:");
            var Password = Console.ReadLine();

            Login(Email, Password);

            //Le e envia mensagem
            ReadMessage();
            SendMessage(Email);
                                           
        }

        private static void Login(string p_Email, string p_Password)
        {
            Console.WriteLine();

            //Quando o campo email for visivel, entro com o email informado    
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id(Resources.LOGIN_ID)));
            var loginBox = driver.FindElement(By.Id(Resources.LOGIN_ID));
            loginBox.SendKeys(p_Email);
            loginBox.SendKeys(Keys.Enter);

            //Quando o campo senha for visivel, entro com o a senha informada
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id(Resources.PASSWORD_ID)));         
            var pwBox = driver.FindElement(By.Name(Resources.PASSWORD_ID));
            pwBox.SendKeys(p_Password);
            pwBox.SendKeys(Keys.Enter);

        }

     
        private static void ReadMessage()
        {
            //Seleciono o primeiro Email da Lista
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(Resources.FIRST_EMAIL_XPATH)));
            var FirstEmail = driver.FindElement(By.XPath(Resources.FIRST_EMAIL_XPATH));

            Console.WriteLine();
            Console.WriteLine("************************************************************");
            Console.WriteLine("Lendo o Primeiro Email.. ");
            Console.WriteLine();

            //Pego o Remetente
            var Sender = driver.FindElement(By.XPath(Resources.SENDER_XPATH));
            Console.WriteLine("De: " + Sender.Text);
            Console.WriteLine();

            //Abro o Email        
            FirstEmail.Click();

            //Exibo a mensagem
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(Resources.FIRST_EMAIL_MESSAGE_XPATH)));
            var FirstEmailMessage = driver.FindElement(By.XPath(Resources.FIRST_EMAIL_MESSAGE_XPATH));
            Console.WriteLine(FirstEmailMessage.Text);
        }

        private static void SendMessage(string p_email)
        {

            Console.WriteLine();
            Console.WriteLine("************************************************************");
            Console.WriteLine("Enviando Email.. ");

            var newMessage = driver.FindElement(By.XPath(Resources.NEW_EMAIL_XPATH));
            newMessage.Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.Id(Resources.SEND_TO_ID)));
            var sendTo = driver.FindElement(By.Id(Resources.SEND_TO_ID));
            sendTo.SendKeys(p_email);

            var sendSubjact = driver.FindElement(By.Name(Resources.SEND_SUBJACT_NAME));
            sendSubjact.SendKeys("Olá");

            var sendMessage = driver.FindElement(By.Name(Resources.SEND_MESSAGE_NAME));
            sendMessage.SendKeys("Olá Mundo");

            driver.FindElement(By.XPath(Resources.SEND_SUBMIT_XPATH)).Click();

            Console.WriteLine();
            Console.WriteLine("Email Enviado Com sucesso!");

        }
    }
}
