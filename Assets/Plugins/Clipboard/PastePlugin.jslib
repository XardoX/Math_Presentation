var PastePlugin = {
  ClipboardReader: function(gObj, vName) {
    try {
      var gameObjectName = UTF8ToString(gObj);
      var voidName = UTF8ToString(vName);
      navigator.clipboard.readText().then(function(data) {
        gameInstance.SendMessage(gameObjectName, voidName, data);
      })
    } catch (e) {
      // Clipboard API not available
        console.log('Failed to read clipboard contents. Clipboard API not available on this browser.');
    }
  }
};
mergeInto(LibraryManager.library, PastePlugin);