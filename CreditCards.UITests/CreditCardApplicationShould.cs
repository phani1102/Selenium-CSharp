using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using Xunit.Abstractions;
using System.Collections.ObjectModel;

namespace CreditCards.UITests
{
    [Trait("Category", "Applications")]
    public class CreditCardApplicationShould
    {
        const string HomeUrl = "http://localhost:44108/";
        const string ApplyUrl = "http://localhost:44108/Apply";

        private readonly ITestOutputHelper output;

        public CreditCardApplicationShould(ITestOutputHelper _output)
        {
            this.output = _output;
        }

        [Fact]
        public void BeInitiatedFromHomePage_NewLowRate()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                IWebElement applyLink =
                 driver.FindElement(By.Name("ApplyLowRate"));
                applyLink.Click();
                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }

        [Fact]
        public void BeInitiatedFromHomePage_EasyApplication()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                IWebElement carouselNext =
                driver.FindElement(By.CssSelector("[data-slide='next']"));
                carouselNext.Click();

                //DemoHelper.Pause(1000);
                //IWebElement applyLink =
                // driver.FindElement(By.LinkText("Easy: Apply Now!"));
                //applyLink.Click();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
                IWebElement applyLink = wait.Until((d) => d.FindElement(By.LinkText("Easy: Apply Now!")));

                applyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }

        [Fact]
        public void BeInitiatedFromHomePage_CustomerService()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                // ******************* 1 *********************
                //driver.Navigate().GoToUrl(HomeUrl);

                //DemoHelper.Pause();

                //IWebElement carouselNext =
                //driver.FindElement(By.CssSelector("[data-slide='next']"));
                //carouselNext.Click();
                //DemoHelper.Pause(1000);
                //carouselNext.Click();
                //DemoHelper.Pause(1000);

                // ******************* 2 *********************
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Setting implicit wait");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(35);

                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Navigating to '{HomeUrl}'");
                driver.Navigate().GoToUrl(HomeUrl);

                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Finding element");
                IWebElement applyLink =
                 driver.FindElement(By.ClassName("customer-service-apply-now"));

                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Found element Displayed='{applyLink.Displayed}' Enabled='{applyLink.Enabled}'");

                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Clicking element");
                applyLink.Click();
                DemoHelper.Pause();
                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }

        [Fact]
        public void DisplayProductsAndRates()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();


                IWebElement firstTableCell = driver.FindElement(By.TagName("td"));
                string firstProduct = firstTableCell.Text;

                Assert.Equal("Easy Credit Card", firstProduct);


                ReadOnlyCollection<IWebElement> tableCells = driver.FindElements(By.TagName("td"));

                Assert.Equal("Easy Credit Card", tableCells[0].Text);
                Assert.Equal("20% APR", tableCells[1].Text);
                Assert.Equal("Silver Credit Card", tableCells[2].Text);
                Assert.Equal("18% APR", tableCells[3].Text);
                Assert.Equal("Gold Credit Card", tableCells[4].Text);
                Assert.Equal("17% APR", tableCells[5].Text);


            }
        }

        [Fact]
        public void BeInitiatedFromHomePage_RandomGreeting()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                IWebElement randomGreetingApplyLink =
               driver.FindElement(By.PartialLinkText("- Apply Now!"));
                randomGreetingApplyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }

        [Fact]
        public void BeInitiatedFromHomePage_RandomGreeting_Using_XPATH()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                //  IWebElement randomGreetingApplyLink =
                //driver.FindElement(By.XPath("/html/body/div/div[4]/div/p/a"));

                IWebElement randomGreetingApplyLink =
              driver.FindElement(By.XPath("//a[text() [contains(.,'- Apply Now!')]]"));

                randomGreetingApplyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }

        [Fact]
        public void BeInitiatedFromHomePage_EasyApplication_Prebuilt_Conditions()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);
                DemoHelper.Pause();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(11));

                IWebElement applyLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Easy: Apply Now!")));
                applyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);
            }
        }
    }
}
