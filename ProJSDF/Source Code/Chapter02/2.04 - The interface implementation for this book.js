/*For this book, we are using a combination of the first and third approaches. We use comments
to declare what interfaces a class supports, thus improving reusability and improving documentation.
We use the Interface helper class and the class method Interface.ensureImplements
to perform explicit checking of methods. We return useful error messages when an object does
not pass the check.
Here is an example of our Interface class and comment combination:*/
// Interfaces.

var Composite = new Interface('Composite', ['add', 'remove', 'getChild']);
var FormItem = new Interface('FormItem', ['save']);

// CompositeForm class

var CompositeForm = function(id, method, action) { // implements Composite, FormItem
   ...
};

...

function addForm(formInstance) {
    Interface.ensureImplements(formInstance, Composite, FormItem);
    // This function will throw an error if a required method is not implemented,
    // halting execution of the function.
    // All code beneath this line will be executed only if the checks pass.
    ...
}	
/*Interface.ensureImplements provides a strict check. If a problem is found, an error will be
thrown, which can either be caught and handled or allowed to halt execution. Either way, the
programmer will know immediately that there is a problem and where to go to fix it.*/