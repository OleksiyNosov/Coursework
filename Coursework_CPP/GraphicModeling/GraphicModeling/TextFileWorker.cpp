#include "stdafx.h"
#include "TextFileWorker.h"


TextFileWorker::TextFileWorker(string filename)
{
	this->filename = filename;
}


TextFileWorker::~TextFileWorker()
{
}

vector<Shape>* TextFileWorker::GetShapes()
{
	auto shapes = new vector<Shape>();

	freopen("input.txt", "r", stdin);

	

	return shapes;
}
