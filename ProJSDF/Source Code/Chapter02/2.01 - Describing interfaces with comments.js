/*
Describing Interfaces with Comments
The easiest and least effective way of emulating an interface is with comments. Mimicking the
style of other object-oriented languages, the interface and implements keywords are used but
are commented out so they do not cause syntax errors. Here is an example of how these keywords
can be added to code to document the available methods

interface Composite {
    function add(child);
    function remove(child);
    function getChild(index);
}

interface FormItem {
    function save();
}

*/

var CompositeForm = function(id, method, action) { // implements Composite, FormItem
    ...
};

// Implement the Composite interface.

CompositeForm.prototype.add = function(child) {
    ...
};
CompositeForm.prototype.remove = function(child) {
    ...
};
CompositeForm.prototype.getChild = function(index) {
    ...
};

// Implement the FormItem interface.

CompositeForm.prototype.save = function() {
    ...
};

/*This doesn’t emulate the interface functionality very well. There is no checking to ensure
that CompositeForm actually does implement the correct set of methods. No errors are thrown
to inform the programmer that there is a problem. It is really more documentation than anything
else. All compliance is completely voluntary.
That being said, there are some benefits to this approach. It’s easy to implement, requiring
no extra classes or functions. It promotes reusability because classes now have documented
interfaces and can be swapped out with other classes implementing the same ones. It doesn’t
affect file size or execution speed; the comments used in this approach can be trivially stripped
out when the code is deployed, eliminating any increase in file size caused by using interfaces.
However, it doesn’t help in testing and debugging since no error messages are given.*/