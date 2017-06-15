/*
trong js,mọi thứ là đối tượng(ngoại trừ 3 kiểu dữ liệu chính, và ngay cả khi chúng được tự động gói 
trong các đối tượng khi cần).Hơn nữa, tất cả các đối tượng có thể thay đổi được.
2 điều này cho phép sử dụng 1 số kỹ thuật mà các ngôn ngữ khác ko thể vd như gán thuộc tính cho hàm.
In JavaScript, everything is an object (except for the three primitive datatypes, and even they
are automatically wrapped with objects when needed). Furthermore, all objects are mutable.
These two facts mean you can use some techniques that wouldn’t be allowed in most other
languages, such as giving attributes to functions:*/
function displayError(message) {
  displayError.numTimesExecuted++;
  alert(message);
};
displayError.numTimesExecuted = 0;


/* nó cũng có nghĩa có thể chỉnh sửa class sau khi chúng được đã định nghĩa và 
các đối tượng sau khi chúng đã được khởi tạo 
 * It also means you can modify classes after they have been defined and objects after they
have been instantiated:
  Class Person. */
/*Tạo class Person với 2 thuộc tính tên & tuổi*/
function Person(name, age) {
  this.name = name;
  this.age = age;
}
/*Tạo 2 hàm lấy tên & tuổi*/
Person.prototype = {
  getName: function() {
    return this.name;
  },
  getAge: function() {
    return this.age;
  }
}
/* Sử dụng: Tạo mới 1 object*/
/* Instantiate the class. */

var alice = new Person('Alice', 93);
var bill = new Person('Bill', 30);

/* 
 * chỉnh sửa class
 Modify the class. */

Person.prototype.getGreeting = function() {
  return 'Hi ' + this.getName() + '!';
};

/* 
 * chỉnh sửa hàm đã khởi tạo
 Modify a specific instance. */

alice.displayGreeting = function() {
  alert(this.getGreeting());
}
