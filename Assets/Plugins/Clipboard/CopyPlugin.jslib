var CopyPlugin = {
  ClipboardWriter: function(newClipText) {
      try {
        var clipText = UTF8ToString(newClipText);
      navigator.clipboard.writeText(clipText);
    } catch (e) {
      // Clipboard API not available
         console.log('Failed to write to clipboard. Clipboard API not available on this browser.');
    }
  }
};
mergeInto(LibraryManager.library, CopyPlugin);