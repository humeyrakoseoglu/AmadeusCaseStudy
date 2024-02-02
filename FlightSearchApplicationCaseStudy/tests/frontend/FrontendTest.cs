using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

/**
 * @author Humeyra Koseoglu
 * @since 1.02.2024
 */

namespace FlightSearchApplicationCaseStudy.tests.frontend
{
    public class FrontentTests
    {
        private IWebDriver driver;

        // Sample city names for testing purposes
        private String city = "Istanbul";
        private String city2 = "Los Angeles";
        private String city3 = "Paris";
        private String city4 = "London";

        // CSS selectors for various elements on the web page
        private String cssSelectorFromInput = "input[id='headlessui-combobox-input-:Rq9lla:']";
        private String cssSelectorToInput = "input[id='headlessui-combobox-input-:Rqhlla:']";
        private String cssSelectorMessageElement = "div[class='mt-24 max-w-5xl w-full justify-center items-center text-sm lg:flex']";
        private String cssSelectorFlightItems = "ul.grid li";
        private String cssSelectorFlightItemsCard = ".max-w-5xl ul.grid li";
        private String cssSelectorToOptions = "button[id='headlessui-combobox-button-:R1a9lla:']";
        private String cssSelectorFoundItemsText = "p[class='mb-10']";

        // Initialize the WebDriver before each test
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://flights-app.pages.dev/");
        }

        //  Search for flights between different cities 
        //  From Istanbul to Los Angeles
        [Test]
        public void SearchForDifferentCityExistFlightTest()
        {
            Actions actions = new Actions(driver);

            // Enter the different value in "From" and "To" input fields
            IWebElement fromInput = driver.FindElement(By.CssSelector(cssSelectorFromInput));
            actions.MoveToElement(fromInput).Click().SendKeys(city).SendKeys(Keys.Return).Build().Perform();

            // Wait for 2 seconds before selecting "To" field
            System.Threading.Thread.Sleep(2000);

            IWebElement toInput = driver.FindElement(By.CssSelector(cssSelectorToInput));
            actions.MoveToElement(toInput).Click().SendKeys(city2).SendKeys(Keys.Return).Build().Perform();

            // Assert that two flights are listed
            IReadOnlyCollection<IWebElement> flightItems = driver.FindElements(By.CssSelector(cssSelectorFlightItems));
            Assert.That(flightItems, Has.Count.EqualTo(2));
        }

        // Search for flights between different cities and assert a specific message when no flights are found
        // From Istanbul to Paris
        [Test]
        public void SearchForDifferentCityNotFoundFlightTest()
        {
            Actions actions = new Actions(driver);

            IWebElement fromInput = driver.FindElement(By.CssSelector(cssSelectorFromInput));
            actions.MoveToElement(fromInput).Click().SendKeys(city).SendKeys(Keys.Return).Build().Perform();

            // Wait for 2 seconds before selecting "To" field
            System.Threading.Thread.Sleep(2000);

            IWebElement toInput = driver.FindElement(By.CssSelector(cssSelectorToInput));
            actions.MoveToElement(toInput).Click().SendKeys(city3).SendKeys(Keys.Return).Build().Perform();

            // Assert the expected message when no flights are found
            string expectedMessage = "Bu iki þehir arasýnda uçuþ bulunmuyor. Baþka iki þehir seçmeyi deneyebilirsiniz.";
            IWebElement messageElement = driver.FindElement(By.CssSelector(cssSelectorMessageElement));
            string actualMessage = messageElement.Text.Trim();

            Assert.That(actualMessage, Is.EqualTo(expectedMessage), $"Beklenen mesaj: '{expectedMessage}', Gerçek mesaj: '{actualMessage}'");
        }

        // Checking that the same value cannot be entered in the “From” and “To” input fields
        // From Istanbul to Istanbul
        [Test]
        public void SearchForSameCityTest()
        {
            Actions actions = new Actions(driver);

            // Enter the same value in "From" and "To" input fields
            IWebElement fromInput = driver.FindElement(By.CssSelector(cssSelectorFromInput));
            actions.MoveToElement(fromInput).Click().SendKeys(city).SendKeys(Keys.Return).Build().Perform();

            // Wait for 2 seconds before selecting "To" field
            System.Threading.Thread.Sleep(2000);

            IWebElement toInput = driver.FindElement(By.CssSelector(cssSelectorToInput));
            //ClickOnOption(fromInput, "Istanbul");
            actions.MoveToElement(toInput).Click().SendKeys(city).SendKeys(Keys.Return).Build().Perform();

            // Assert that "To" field options are not available
            IReadOnlyCollection<IWebElement> toOptions = driver.FindElements(By.CssSelector(cssSelectorToOptions));
            Assert.That(toOptions.Count, Is.EqualTo(0), "Ayný þehirin To alanýnda seçilebildiði bir hata oluþtu.");
        }

        // Checking that number X and the number of flights listed in the “Found X items” text are the same
        // From Istanbul to London
        [Test]
        public void ListTest()
        {
            Actions actions = new Actions(driver);

            // Select cities for "From" and "To" fields
            IWebElement fromInput = driver.FindElement(By.CssSelector(cssSelectorFromInput));
            actions.MoveToElement(fromInput).Click().SendKeys(city).SendKeys(Keys.Return).Build().Perform();

            // Wait for 2 seconds before selecting "To" field
            System.Threading.Thread.Sleep(2000);

            IWebElement toInput = driver.FindElement(By.CssSelector(cssSelectorToInput));
            actions.MoveToElement(toInput).Click().SendKeys(city4).SendKeys(Keys.Return).Build().Perform();

            // Wait for the page to automatically perform the search
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //IWebElement searchResultElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.CssSelector("div[class='mb-10']")));

            // Compare the X in "Found X items" with the actual number of listed flights
            int actualFlightCount = GetCardFlightCount();

            if (actualFlightCount > 0)
            {
                int expectedFlightCount = GetFoundXFlightCount();
                Assert.That(actualFlightCount, Is.EqualTo(expectedFlightCount), $"Beklenen uçuþ sayýsý: {expectedFlightCount}, Gerçek uçuþ sayýsý: {actualFlightCount}");
            }
            else
            {
                // Assert the message when no flights are found
                string expectedMessage = "Bu iki þehir arasýnda uçuþ bulunmuyor. Baþka iki þehir seçmeyi deneyebilirsiniz.";
                IWebElement messageElement = driver.FindElement(By.CssSelector(cssSelectorMessageElement));
                string actualMessage = messageElement.Text.Trim();

                Assert.That(actualMessage, Is.EqualTo(expectedMessage), $"Beklenen mesaj: '{expectedMessage}', Gerçek mesaj: '{actualMessage}'");
            }
        }

        // Close the browser at the end of the test
        [TearDown]
        public void TearDown()
        {
            System.Threading.Thread.Sleep(5000);// wait 5 seconds before closing the browser
            driver.Quit();
        }

        // Determine the number of flights from the text "Found X items"
        private int GetFoundXFlightCount()
        {
            var foundItemsText = driver.FindElement(By.CssSelector(cssSelectorFoundItemsText)).Text;
            int expectedCount = int.Parse(foundItemsText.Split(' ')[1]);
            //Console.WriteLine(expectedCount.ToString());
            return expectedCount;
        }

        // Determine the actual number of flights from the list
        private int GetCardFlightCount()
        {
            IReadOnlyCollection<IWebElement> flightItems = driver.FindElements(By.CssSelector(cssSelectorFlightItemsCard));
            // Console.WriteLine(flightItems.Count.ToString());
            return flightItems.Count;
        }
    }
}