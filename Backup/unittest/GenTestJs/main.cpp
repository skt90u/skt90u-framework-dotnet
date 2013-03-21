// GenBigJs.cpp : 定義主控台應用程式的進入點。
//

#include <stdio.h>
#include <string.h>
#include <iostream>
#include <limits>
#include <string>
using namespace std;

char* path = "E:\\GoogleProjectHosting\\jelly-dotnet-framework\\unittest\\testWebControl\\";
int sizeBigJs = 2048;
int sizeMiddleJs = 1024;
int sizeSmallJs = 512;
void GenJsFile(char* filename,int size);

int main(int argc, char* argv[])
{
	GenJsFile("BigJs", sizeBigJs);
	GenJsFile("MiddleJs", sizeMiddleJs);
	GenJsFile("SmallJs", sizeSmallJs);
	return 0;
}

void GenJsFile(char* filename,int size){
	char filepath[256] = {0};
	sprintf(filepath, "%s%s.js", path, filename);
	FILE* fp = fopen(filepath, "w+");
	
	char* s = new char[size];

	fprintf(fp, "function funcA%s(){\n", filename);
	for(int i=0; i<26; i++){
		memset(s, 0, size);
		for(int j=0; j<size-1;j++){
		s[j] = 'a' + i;
		fprintf(fp, "var %s;\n", s);
		}
	}
	fprintf(fp, "alert('this is funcA%s');\n", filename);
	fprintf(fp, "}\n");
	fprintf(fp, "function funcB%s(){ alert('this is funcB%s');}\n", filename, filename);
	fprintf(fp, "alert('%s.js load complete');\n", filename);

	delete [] s;
	fclose(fp);
}