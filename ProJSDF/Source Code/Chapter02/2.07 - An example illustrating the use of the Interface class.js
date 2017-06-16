/*Imagine that you have created a class to take some automated test results and format them for
viewing on a web page. This class’s constructor takes an instance of the TestResult class as an
argument. It then formats the data encapsulated in the TestResult object and outputs it on
request. Here is what the ResultFormatter class looks like initially:*/
// ResultFormatter class, before we implement interface checking.

var ResultFormatter = function(resultsObject) {
  if(!(resultsObject instanceOf TestResult)) {
    throw new Error('ResultsFormatter: constructor requires an instance '
      + 'of TestResult as an argument.');
  }
  this.resultsObject = resultsObject;
};

ResultFormatter.prototype.renderResults = function() {
  var dateOfTest = this.resultsObject.getDate();
  var resultsArray = this.resultsObject.getResults();
  
  var resultsContainer = document.createElement('div');

  var resultsHeader = document.createElement('h3');
  resultsHeader.innerHTML = 'Test Results from ' + dateOfTest.toUTCString();
  resultsContainer.appendChild(resultsHeader);
  
  var resultsList = document.createElement('ul');
  resultsContainer.appendChild(resultsList);
  
  for(var i = 0, len = resultsArray.length; i < len; i++) {
    var listItem = document.createElement('li');
    listItem.innerHTML = resultsArray[i];
    resultsList.appendChild(listItem);
  }
  
  return resultsContainer;
};
/*This class performs a check in the constructor to ensure that the argument is really an
instance of TestResult; if it isn’t, an error is thrown. This allows you to code the renderResults
method knowing confidently that the getDate and getResults methods will be available to
you. Or does it? In the constructor, you are only checking that the resultsObject is an instance
of TestResult. That does not actually ensure that the methods you need are implemented.
TestResult could be changed so that it no longer has a getDate method. The check in the constructor
would pass, but the renderResults method would fail.
The check in the constructor is also unnecessarily limiting. It prevents instances of other
classes from being used as arguments, even if they would work perfectly fine. Say, for example,
you have a class named WeatherData. It has a getDate and a getResults method and could be
used in the ResultFormatter class without a problem. But using explicit type checking (with
the instanceOf operator) would prevent any instances of WeatherData from being used.
The solution is to remove the instanceOf check and replace it with an interface. The first
step is to create the interface itself:*/

// ResultSet Interface.

var ResultSet = new Interface('ResultSet', ['getDate', 'getResults']);
/*This line of code creates a new instance of the Interface object. The first argument is the
name of the interface, and the second is an array of strings, where each string is the name of
a required method. Now that you have the interface, you can replace the instanceOf check
with an interface check:*/
// ResultFormatter class, after adding Interface checking.

var ResultFormatter = function(resultsObject) {
  Interface.ensureImplements(resultsObject, ResultSet);
  this.resultsObject = resultsObject;
};

ResultFormatter.prototype.renderResults = function() {
  ...
};
/*The renderResults method remains unchanged. The constructor, on the other hand, has
been modified to use ensureImplements instead of instanceOf. You could now use an instance
of WeatherData in this constructor, or any other class that implements the needed methods. By
changing a few lines of code within the ResultFormatter class, you have made the check more
accurate (by ensuring the required methods have been implemented) and more permissive
(by allowing any object to be used that matches the interface).*/

/*Patterns That Rely on the Interface
The following is a list of a few of the patterns, which we discuss in later chapters, that especially
rely on an interface implementation to work:
• The factory pattern: The specific objects that are created by a factory can change
depending on the situation. In order to ensure that the objects created can be used
interchangeably, interfaces are used. This means that a factory is guaranteed to produce
an object that will implement the needed methods.
• The composite pattern: You really can’t use this pattern without an interface. The most
important idea behind the composite is that groups of objects can be treated the same
as the constituent objects. This is accomplished by implementing the same interface.
Without some form of duck typing or type checking, the composite loses much of its
power.
• The decorator pattern: A decorator works by transparently wrapping another object. This
is accomplished by implementing the exact same interface as the other object; from the
outside, the decorator and the object it wraps look identical. We use the Interface class
to ensure that any decorator objects created implement the needed methods.
• The command pattern: All command objects within your code will implement the same
methods (which are usually named execute, run, or undo). By using interfaces, you can
create classes that can execute these commands without needing to know anything about
them, other than the fact that they implement the correct interface. This allows you to
create extremely modular and loosely coupled user interfaces and APIs.
The interface is an important concept that we use throughout this book. It’s worth playing
around with interfaces to see if your specific situation warrants their use.
Summary
In this chapter, we explored the way that interfaces are used and implemented in popular
object-oriented languages. We showed that all different implementations of the concept of the
interface share a couple features: a way of specifying what methods to expect, and a way to check
that those methods are indeed implemented, with helpful error messages if they are not. We
are able to emulate these features with a combination of documentation (in comments), a helper
class, and duck typing. The challenge is in knowing when to use this helper class. Interfaces
are not always needed. One of JavaScript’s greatest strengths is its flexibility, and enforcing
strict type checking where it is not needed reduces this flexibility. But careful use of the
Interface class can create more robust classes and more stable code.*/