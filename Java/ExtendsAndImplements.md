extends是继承父类，只要那个类不是声明为final或者那个类定义为abstract的就能继承;

JAVA中不支持多重继承，但是可以用接口来实现，这样就要用到implements;

继承只能继承一个类，但implements可以实现多个接口，用逗号分开就行了
比如: class A extends B implements C,D,E