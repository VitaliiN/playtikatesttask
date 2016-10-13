using Moq;
using NUnit.Framework;
using PlaytikaTestTask.BI.Interfaces;
using PlaytikaTestTask.BI.Implementation;
using PlaytikaTestTask.Constants;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestFixture]
    public class DirectoryServiceUnitTests
    {
        private Mock<IFileSystemWrapper> fileSystemWrapper;
        private Mock<IFileNameTransformerBuilder> fileNameTransformerBuilder;
        private Mock<IFileNameTransformer> fileNameTransformer;
        private DirectoryService directoryService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            fileSystemWrapper = new Mock<IFileSystemWrapper>();
            fileNameTransformerBuilder = new Mock<IFileNameTransformerBuilder>();
            fileNameTransformer = new Mock<IFileNameTransformer>();
            directoryService = new DirectoryService(fileSystemWrapper.Object, fileNameTransformerBuilder.Object);
        }

        [SetUp]
        public void SetUp()
        {
            fileSystemWrapper.ResetCalls();
            fileNameTransformerBuilder.ResetCalls();
            fileNameTransformer.ResetCalls();
        }

        [OneTimeTearDown]
        public void TestFixtureTearDown()
        {
            fileSystemWrapper.VerifyAll();
            fileNameTransformerBuilder.VerifyAll();
            fileNameTransformer.VerifyAll();
        }

        [Test]
        public void SelectedFolderEmpty()
        {
            //Arrange
            string path = "somePath";
            string dest = "dest";
            var all = AvailableActions.all;

            fileNameTransformerBuilder
                .Setup(x => x.GetTransformer(all))
                .Returns(fileNameTransformer.Object);

            fileSystemWrapper
                .Setup(x => x.GetFilesNames(path, all))
                .Returns(Task.FromResult(new List<string>()));

            fileSystemWrapper
                .Setup(x => x.GetInnerDirectoriesPaths(path))
                .Returns(Task.FromResult(new List<string>()));
            directoryService.RootFolder = "empty";

            //Act
            directoryService.Scan(path, all, dest).GetAwaiter().GetResult();

            // Verify
            fileNameTransformerBuilder.Verify(x => x.GetTransformer(all), Times.Once());
            fileSystemWrapper.Verify(x => x.GetFilesNames(path, all), Times.Once());
            fileSystemWrapper.Verify(x => x.AppendAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
            fileNameTransformer.Verify(x => x.Transform(It.IsAny<string>()), Times.Never());
            fileSystemWrapper.Verify(x => x.GetInnerDirectoriesPaths(path), Times.Once());
        }


        [Test]
        public void SelectedFolder_Contains_2Files_And_1InnerEmptyFolder()
        {
            //Arrange
            string path = "somePath";
            string dest = "dest";
            var all = AvailableActions.all;
            List<string> files = new List<string> { "file1", "file2" };
            List<string> innerFolders = new List<string> { "folder" };
            fileNameTransformerBuilder
                .Setup(x => x.GetTransformer(all))
                .Returns(fileNameTransformer.Object);

            fileSystemWrapper
                .SetupSequence(x => x.GetFilesNames(It.IsAny<string>(), all))
                .Returns(Task.FromResult(files))
                .Returns(Task.FromResult(new List<string>()));

            fileSystemWrapper
                .SetupSequence(x => x.GetInnerDirectoriesPaths(It.IsAny<string>()))
                .Returns(Task.FromResult(innerFolders))
                .Returns(Task.FromResult(new List<string>()));
            directoryService.RootFolder = "empty";

            //Act
            directoryService.Scan(path, all, dest).GetAwaiter().GetResult();

            // Verify
            fileNameTransformerBuilder.Verify(x => x.GetTransformer(all), Times.Exactly(2));
            // called 2 times ( for root and inner folder ) 
            fileSystemWrapper.Verify(x => x.GetFilesNames(It.IsAny<string>(), all), Times.Exactly(2));
            // root folder contains 2 files
            fileSystemWrapper.Verify(x => x.AppendAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            // root folder contains 2 files
            fileNameTransformer.Verify(x => x.Transform(It.IsAny<string>()), Times.Exactly(2));
            // called 2 times ( for root and inner folder ) 
            fileSystemWrapper.Verify(x => x.GetInnerDirectoriesPaths(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void VerifyGetFilesReturnNull()
        {
            string path = "somePath";
            string dest = "dest";
            var all = AvailableActions.all;

            fileNameTransformerBuilder
                .Setup(x => x.GetTransformer(all))
                .Returns(fileNameTransformer.Object);

            //returns null
            fileSystemWrapper
                .Setup(x => x.GetFilesNames(path, all))
                .Returns(Task.FromResult(default(List<string>)));

            fileSystemWrapper
                .Setup(x => x.GetInnerDirectoriesPaths(path))
                .Returns(Task.FromResult(new List<string>()));
            directoryService.RootFolder = "empty";

            //Act
            directoryService.Scan(path, all, dest).GetAwaiter().GetResult();

            // Verify
            fileNameTransformerBuilder.Verify(x => x.GetTransformer(all), Times.Once());
            fileSystemWrapper.Verify(x => x.GetFilesNames(path, all), Times.Once());
            fileSystemWrapper.Verify(x => x.AppendAllText(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
            fileNameTransformer.Verify(x => x.Transform(It.IsAny<string>()), Times.Never());
            fileSystemWrapper.Verify(x => x.GetInnerDirectoriesPaths(path), Times.Once());
        }
    }
}
