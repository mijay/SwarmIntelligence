pdflatex thesis.tex
makeindex thesis.nlo -s nomencl.ist -o thesis.nls
bibtex8 -B thesis
pdflatex thesis.tex
texworks thesis.pdf