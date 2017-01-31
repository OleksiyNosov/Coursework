#pragma once

using std::cin;
using std::cout;
using std::string;

class TextFileWorker
{
private:
	string filename;

public:
	TextFileWorker(string filename);
	~TextFileWorker();
};

