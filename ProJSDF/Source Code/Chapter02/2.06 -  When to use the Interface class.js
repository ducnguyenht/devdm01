/*It doesn’t always make sense to use strict type checking. Most JavaScript programmers have
worked for years without ever needing an interface or the kind of checks that it provides. It
becomes most beneficial when you start implementing complex systems using design patterns.
It might seem like interfaces reduce JavaScript’s flexibility, but they actually improve it by allowing
your objects to be more loosely coupled. Your functions can be more flexible because you
can pass in arguments of any type and still ensure that only objects with the needed method
will be used. There are a few situations where interfaces can be useful.
In a large project, with many different programmers writing code, interfaces are essential.
Often programmers are asked to use an API that hasn’t been written yet, or are asked to provide
stubs so the development won’t be delayed. Interfaces can be very valuable in this situation
for several reasons. They document the API and can be used as formal communication between
two programmers. When the stubs are replaced with the production API, you will know immediately
whether the methods you need are implemented. If the API changes in mid-development,
another can be seamlessly put in its place as long as it implements the same interface.
It is becoming increasingly common to include code from Internet domains that you do
not have direct control over. Externally hosted libraries are one example of this, as are APIs to
services such as search, email, and maps. Even when these come from trusted sources, use
caution to ensure their changes don’t cause errors in your code. One way to do this is to create
Interface objects for each API that you rely on, and then test each object you receive to ensure
it implements those interfaces correctly:*/
var DynamicMap = new Interface('DynamicMap', ['centerOnPoint', 'zoom', 'draw']);

function displayRoute(mapInstance) {
    Interface.ensureImplements(mapInstace, DynamicMap);
    mapInstance.centerOnPoint(12, 34);
    mapInstance.zoom(5);
    mapInstance.draw();
    ...
}
/*In this example, the displayRoute function needs the passed-in argument to have three
specific methods. By using an Interface object and calling Interface.ensureImplements, you
will know for sure that these methods are implemented and will see an error if they are not. This
error can be caught in a try/catch block and potentially used to send an Ajax request alerting
you to the problem with the external API. This makes your mash-ups more stable and secure.*/