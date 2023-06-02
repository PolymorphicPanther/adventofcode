import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.List;

public class Main {
    public static void main(String[] args) throws IOException {

        var lines = Files.readAllLines(Path.of("src\\input.txt"));

        var sum = new Part1().calculateSum(lines);
        System.out.println("Part 1: " + sum);

        var part2 = new Part2();
        part2.draw(lines);
    }

}

class Part2 {

    private final char[][] grid;
    private final Sprite sprite;

    private int idx = 0;
    private int row = 0;

    Part2() {
        grid = new char[6][40];
        sprite = new Sprite();
    }


    void draw(List<String> cmds) {
        var row = 0;
        for (String line : cmds) {
            if (row == grid.length) break;

            var cmd = line.split(" ");
            if (cmd[0].trim().equals("noop")) {
                drawIdx(1);
            } else {
                drawIdx(2);

                var addend = Integer.parseInt(cmd[1]);
                sprite.setPos(sprite.getPos() + addend);
            }

        }

        print();

    }

    void drawIdx(int diff) {
        for (var i = 0; i < diff; i++) {
            if (idx == 40) {
                idx = 0;
                ++row;
            }
            if (sprite.shouldDraw(idx)) grid[row][idx] = '#';
            else grid[row][idx] = '.';
            ++idx;
        }
    }

    void print() {
        for (char[] chars : grid) {
            System.out.println(new String(chars));
        }
    }

    static class Sprite {

        private int pos = 1;

        void setPos(int x) {
            pos = x;
        }

        int getPos() {
            return pos;
        }

        boolean shouldDraw(int x) {
            return Math.abs(x - pos) <= 1.0d;
        }
    }
}


class Part1 {
    int calculateSum(List<String> cmds) {
        var sum = 0;
        var i = 0;
        var x = 1;
        var cycles = new int[]{20, 60, 100, 140, 180, 220};
        var cycle = 0;

        for (String line : cmds) {
            if (i == cycles.length) break;

            var cmd = line.split(" ");
            if (cmd[0].trim().equals("noop")) {
                if (cycle + 1 >= cycles[i]) sum += (cycles[i++] * x);

                ++cycle;
            } else {
                if (cycle + 2 >= cycles[i]) sum += (cycles[i++] * x);

                x += Integer.parseInt(cmd[1]);
                cycle += 2;
            }

        }
        return sum;
    }
}
