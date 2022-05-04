## General notes / bugs
- When adding a child, it's former parent should get informed.
	- Right now a work around was to add method UpdateChild(IElement child) to BaseElement to handle such cases.
- Elements and drawables should have a unique ID assigned to them.
	- The unique Id should be used to remove/add them, and make it much more efficent.
- Currently each BaseScene start's the SpriteBatch, this is efficient, but doesn't allow for shaders to be applied
	- A better system would group drawables by their shaders, and apply it, then begin drawin
- There is a memory leak when resizing fonts too fast, this is probably due to double caching by the library first, and then by the engine