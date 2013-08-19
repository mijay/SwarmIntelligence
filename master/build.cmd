pdflatex thesis
makeindex thesis.nlo -s nomencl.ist -o thesis.nls
bibtex8 -B thesis
pdflatex thesis
pdflatex thesis
texworks thesis.pdf