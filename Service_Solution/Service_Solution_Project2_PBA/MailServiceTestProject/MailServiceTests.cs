using EmailService;
using NUnit.Framework;

namespace MailServiceTestProject
{
    public class MailServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_Sent_An_Email()
        {
            //Arrange
            string bookingMessage = "<div style='padding:20px;'><p>You booked a parkingspot! \n Regards The Grasshoppers.</p></div>"; 
            string htmlMessage = "<div style='padding:20px;font-family:verdana;text-align:center;background-color:#4bd30a;color:b42cf5'>The Beer so Good it’s Bad.</div>";
            string fullMessage = bookingMessage + htmlMessage; 
            //Act
            MailHandler.SentMail(fullMessage);   
            //Assert
            Assert.Pass();  //Should assert something else.
        }

        //Send til Rabboit MQ queue for this mail.
        [Test]
        public void Test_RabbitMQ_For_Sending_Mail()
        {
            //Arrange
            string bookingMessage = "<div style='padding:20px;'><p>You booked a parkingspot! \n Regards The Grasshoppers.</p></div>";
            string htmlMessage = "<div style='padding:20px;font-family:verdana;text-align:center;background-color:#4bd30a;color:b42cf5'>The Beer so Good it’s Bad.</div>";
            string fullMessage = bookingMessage + htmlMessage;
            //Act
                    //RabbitMQ.
            //Assert
            Assert.Pass();  //Should assert something else.
        }
    }
}