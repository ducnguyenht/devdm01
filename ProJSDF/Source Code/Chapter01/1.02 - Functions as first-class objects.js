/*
Functions As First-Class Objects
In JavaScript, functions are first-class objects. They can be stored in variables, passed into other
functions as arguments, passed out of functions as return values, and constructed at run-time.
These features provide a great deal of flexibility and expressiveness when dealing with functions.
As you will see throughout the book, these features are the foundation around which you will
build a classically object-oriented framework.
You can create anonymous functions, which are functions created using the function()
{ ... } syntax. They are not given names, but they can be assigned to variables. Here is an
example of an anonymous function:*/

/* 
 * 1 hàm vô danh và được thực thi ngay lập tức
 An anonymous function, executed immediately. */

(function() {
  var foo = 10;
  var bar = 2;
  alert(foo * bar);
})();


/* 
 * 1 hàm vô danh với tham số truyền vào
 An anonymous function with arguments. */

(function(foo, bar) {
  alert(foo * bar);
})(10, 2);


/* 
 * 1 hàm vô danh với tham số truyền vào và trả về giá trị
 An anonymous function that returns a value. */

var baz = (function(foo, bar) {
  return foo * bar;
})(10, 2);

// baz will equal 20.


/* 
 * 1 hàm vô danh sử dụng như là closure
 An anonymous function used as a closure. 
 đinh nghĩa closure:https://freetuts.net/closure-la-gi-closure-function-trong-javascript-758.html
 */

var baz;

(function() {
  var foo = 10;
  var bar = 2;
  baz = function() { 
    return foo * bar; 
  };
})();

baz(); /* 
baz có thể truy cập foo & bar mặc dù nó được thực thi ngoài hàm vô danh.
baz can access foo and bar, even though is it executed outside of the
        anonymous function.*/
