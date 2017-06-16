/*In the end, it doesn�t matter whether a class declares the interfaces it supports, as long as the
required methods are in place. That is where duck typing comes in. Duck typing was named
after the saying, �If it walks like a duck and quacks like a duck, it�s a duck.� It is a technique to
determine whether an object is an instance of a class based solely on what methods it implements,
but it also works great for checking whether a class implements an interface. The idea
behind this approach is simple: if an object contains methods that are named the same as the
methods defined in your interface, it implements that interface. Using a helper function, you
can ensure that the required methods are there:*/
// Interfaces.

var Composite = new Interface('Composite', ['add', 'remove', 'getChild']);
var FormItem = new Interface('FormItem', ['save']);

// CompositeForm class

var CompositeForm = function(id, method, action) {
   ...
};

...

function addForm(formInstance) {
    ensureImplements(formInstance, Composite, FormItem);
    // This function will throw an error if a required method is not implemented.
    ...
}
/*This differs from the other two approaches in that it uses no comments. All aspects of this
are enforceable. The ensureImplements function takes at least two arguments. The first argument
is the object you want to check. The other arguments are the interfaces that the first object will
be compared against. The function checks that the object given as the first argument implements
the methods declared in those interfaces. If any method is missing, an error will be thrown with
a useful message, including both the name of the missing method and the name of the interface
that is incorrectly implemented. This check can be added anywhere in your code that needs to
ensure an interface. In this example, you only want the addForm function to add the form if it
supports the needed methods.
While probably being the most useful of the three methods, it still has some drawbacks.
A class never declares which interfaces it implements, reducing the reusability of the code
and not self-documenting like the other approaches. It requires a helper class, Interface, and
a helper function, ensureImplements. It does not check the names or numbers of arguments
used in the methods or their types, only that the method has the correct name*/