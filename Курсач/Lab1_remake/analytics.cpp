#include "str.h"
#include "structs.h"
#include <iostream>

List<Paragraph> getParagraps(str text)
{
	str endofpar("\u00b6"); // '¶' - символ конца абзаца (https://ru.wikipedia.org/wiki/%D0%97%D0%BD%D0%B0%D0%BA_%D0%B0%D0%B1%D0%B7%D0%B0%D1%86%D0%B0)
	List<str> pars = text.split(endofpar);
	List<Paragraph> ret;
	for (int i = 0; i < pars.count(); i++)
	{
		Paragraph p;
		p.value = pars.at(i)->value;
		ret.add(p);
	}
	return ret;
}
Paragraph analyse(Paragraph p)
{
	//init
	Paragraph ret;
	ret.value = p.value;

	//sentences
	List<str> stc = ret.value.split(str("!?.\u00b6"), false);
	ret.sentences = List<Sentence>();
	for (int i = 0; i < stc.count(); i++)
	{
		Sentence s;
		s.value = stc.at(i)->value;
		s.freq = 1;
		if (ret.sentences.indexOf(s) == -1)
			ret.sentences.add(s);
		else ret.sentences.at(ret.sentences.indexOf(s))->value.freq++;
	}

	//Words
	List<str> wrd = ret.value.split(str("!?,()+=_. \u00b6\n\r\t"), false, true);
	ret.words = List<Word>();
	for (int i = 0; i < wrd.count(); i++)
	{
		Word w;
		w.value = wrd.at(i)->value;
		w.freq = 1;
		if (ret.words.indexOf(w) == -1)
			ret.words.add(w);
		else ret.words.at(ret.words.indexOf(w))->value.freq++;
	}

	//Subwords
	ret.subwords = List<Subword>();
	for (int i = 0; i < ret.words.count(); i++)
	{
		str word = ret.words.at(i)->value.value;
		int wfreq = ret.words.at(i)->value.freq;
		for (int k = 2; k < word.len(); k++)
		{
			for (int j = 0; j + k <= word.len(); j++)
			{
				Subword sb;
				for (int t = 0; t < k; t++)
					sb.value += word.value[j + t];
				sb.freq = wfreq;
				if (ret.subwords.indexOf(sb) == -1)
					ret.subwords.add(sb);
				else ret.subwords.at(ret.subwords.indexOf(sb))->value.freq += sb.freq;
			}
		}
	}

	//Chars
	ret.chars = List<Char>();
	for (int i = 0; i < ret.value.len(); i++)
	{
		Char ch;
		ch.value = ret.value.value[i];
		ch.freq = 1;
		if (ret.chars.indexOf(ch) == -1)
			ret.chars.add(ch);
		else ret.chars.at(ret.chars.indexOf(ch))->value.freq++;
	}

	return ret;
}
Paragraph analyse(str p)
{
	Paragraph ret;
	ret.value = p;
	return analyse(ret);
}