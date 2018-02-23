//The MIT License (MIT)

//Copyright (c) 2018 Twan Veldhuis, Ivar Troost

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.


//start settings
$health = 5
$score = 0
$length = 16

//script
var o = choose 0 1
spawn raft -0.3
spawn raft 0.3

spawn bomb 0 0.05 3 8 1
wait 4

var cloudLeft = spawn cloud -0.5 1.4
var cloudRight = spawn cloud 0.5 1.4
var starMiddle = spawn star 0 1
wait 2

despawn starMiddle
wait 3

var starMiddle2 = spawn star 0 1
wait 2

var x1array = array Float -0.6 0.6
var x2array = array Float -0.9 0.9
var x3array = array Float 0.9 -0.9
var x1 = select o x1array
var x2 = select o x2array
var x3 = select o x3array
spawn bomb x1 0.05 1.8 2 1
spawn bomb x2 0.05 1.8 2 1
spawn bomb x3 0.05 3.5 2 1
wait 6


