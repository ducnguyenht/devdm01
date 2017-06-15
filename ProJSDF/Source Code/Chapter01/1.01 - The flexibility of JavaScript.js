/*The Flexibility of JavaScript
 * One of the most powerful features of the language is its flexibility. As a JavaScript programmer,
you can make your programs as simple or as complex as you wish them to be. The language
also allows several different programming styles. You can write your code in the functional style
or in the slightly more complex object-oriented style. It also lets you write relatively complex
programs without knowing anything at all about functional or object-oriented programming;
you can be productive in this language just by writing simple functions. This may be one of the
reasons that some people see JavaScript as a toy, but we see it as a good thing. It allows programmers
to accomplish useful tasks with a very small, easy-to-learn subset of the language. It also
means that JavaScript scales up as you become a more advanced programmer.
JavaScript allows you to emulate patterns and idioms found in other languages. It even
creates a few of its own. It provides all the same object-oriented features as the more traditional
server-side languages.
Let’s take a quick look at a few different ways you can organize code to accomplish one
task: starting and stopping an animation. It’s OK if you don’t understand these examples; all of
the patterns and techniques we use here are explained throughout the book. For now, you can
view this section as a practical example of the different ways a task can be accomplished in
JavaScript.
 */
/* Tạo hàm */
/* Start and stop animations using functions. */

function startAnimation() {
  ...
}

function stopAnimation() {
  ...
}


/* Tạo class*/
/* Anim class. */

var Anim = function() {
  ...
};
Anim.prototype.start = function() {
  ...
};
Anim.prototype.stop = function() {
  ...
};
/* Sử dụng hàm trong class*/
/* Usage. */

var myAnim = new Anim();
myAnim.start();
...
myAnim.stop();


/* Tạo class và 1 kiểu khai báo khác cho hàm trong class*/
/* Anim class, with a slightly different syntax for declaring methods. */

var Anim = function() { 
  ...
};
Anim.prototype = {
  start: function() {
    ...
  },
  stop: function() {
    ...
  }
};


/* Thêm 1 phương thức tạo hàm để tái sử dụng bằng cách khai báo tên phương thức.*/
/* Add a method to the Function class that can be used to declare methods. */

Function.prototype.method = function(name, fn) {
  this.prototype[name] = fn;
};
/* Sử dụng phương thức theo cách tiện lợi hơn theo oop*/
/* Anim class, with methods created using a convenience method. */

var Anim = function() { 
  ...
};
Anim.method('start', function() {
  ...
});
Anim.method('stop', function() {
  ...
});


/* Cách khai báo cho phép gọi hàm liên tiếp.*/
/* This version allows the calls to be chained. */

Function.prototype.method = function(name, fn) {
    this.prototype[name] = fn;
    return this;
};
/* Sử dụng cách gọi hàm liên tiếp.*/
/* Anim class, with methods created using a convenience method and chaining. */

var Anim = function() { 
  ...
};
Anim.
  method('start', function() {
    ...
  }).
  method('stop', function() {
    ...
  });

/*A Loosely Typed Language
 * In JavaScript, you do not declare a type when defining a variable. However, this does not mean
that variables are not typed. Depending on what data it contains, a variable can have one of
several types. There are three primitive types: booleans, numbers, and strings (JavaScript differs
from most other mainstream languages in that it treats integers and floats as the same type).
There are functions, which contain executable code. There are objects, which are composite
datatypes (an array is a specialized object, which contains an ordered collection of values).
Lastly, there are the null and undefined datatypes. Primitive datatypes are passed by value,
while all other datatypes are passed by reference. This can cause some unexpected side effects
if you aren’t aware of it.
As in other loosely typed languages, a variable can change its type, depending on what
value is assigned to it. The primitive datatypes can also be cast from one type to another. The
toString method converts a number or boolean to a string. The parseFloat and parseInt functions
convert strings to numbers. Double negation casts a string or a number to a boolean:
var bool = !!num;
Loosely typed variables provide a great deal of flexibility. Because JavaScript converts type
as needed, for the most part, you won’t have to worry about type errors.
 */