
#include <cstdio>
#include <cstring>
#include <iostream>
#include <ostream>

void f2()
{
	printf("f2\n");
}

void f1(char* c)
{
	char buf[16];
	strcpy(buf, c);
	printf("f1\n");
}



int main()
{
	f1((char*)"aaa");

	std::cout << &f2<< std::endl;

	char buf[16 + 9];
	//not nulls
	for (int i = 0; i < 20; i++)
		buf[i] = 0x01;

	//addr f2
	int* ptr = (int*)&(buf[20]);
	*ptr = 0x00411168;

	//end
	buf[24] = (char)0x00;
	
	f1(buf);

	return 0;
}