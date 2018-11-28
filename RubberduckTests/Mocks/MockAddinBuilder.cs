using System.Collections.Generic;
using System.Collections.ObjectModel;
using Moq;
using Rubberduck.VBEditor;
using Rubberduck.VBEditor.SafeComWrappers.Abstract;

namespace RubberduckTests.Mocks
{
    public class MockAddInBuilder
    {
        private readonly Mock<IAddIn> _addIn;

        public MockAddInBuilder()
        {
            _addIn = CreateAddInMock();
        }

        private Mock<IAddIn> CreateAddInMock()
        {
            var addIn = new Mock<IAddIn>();

            addIn.Setup(a => a.MenuBarLocations).Returns(new ReadOnlyDictionary<KnownMenuBar, CommandBarLocation>(new Dictionary<KnownMenuBar, CommandBarLocation>
            {
                {KnownMenuBar.Main, new CommandBarLocation(1, 1)},
                {KnownMenuBar.CodeWindow, new CommandBarLocation(2, 2)},
                {KnownMenuBar.ProjectExplorer, new CommandBarLocation(3, 3)},
                {KnownMenuBar.MsForm, new CommandBarLocation(4, 4)},
                {KnownMenuBar.MsFormControl, new CommandBarLocation(5, 5)}
            }));

            return addIn;
        }

        public Mock<IAddIn> Build()
        {
            return _addIn;
        }
    }
}
