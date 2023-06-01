// Problem statement: https://adventofcode.com/2022/day/9

package org.aoc;

import java.io.IOException;
import java.net.URISyntaxException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.HashSet;
import java.util.Set;

public class Main {
    public static void main(String[] args) throws IOException, URISyntaxException {
        var ropeLen = 10;
        var rope = new Rope(ropeLen);
        var lines = Files.readAllLines(Path.of(Rope.class.getClassLoader().getResource("input.txt").toURI()));
        for (var line : lines) {
            var cmd = line.split(" ");
            var direction = cmd[0];
            var magnitude = Integer.parseInt(cmd[1]);

            switch (direction) {
                case "R" -> rope.right(magnitude);
                case "L" -> rope.left(magnitude);
                case "U" -> rope.up(magnitude);
                case "D" -> rope.down(magnitude);
                default -> throw new RuntimeException("Unknown command");
            }

        }
        System.out.format("Tail visited %d positions\n", rope.getTailPositionsVisitedCount());
    }

    static class Rope {
        private final HeadNode headNode;

        public Rope(int len) {
            headNode = new HeadNode();
            Node currentNode = headNode;
            for (int i = 0; i < len - 1; i++) {
                var next = new Node();
                currentNode.setNext(next);
                currentNode = next;
            }
        }

        public int getTailPositionsVisitedCount() {
            Node current = headNode;
            while (current.next != null)
                current = current.next;

            return current.positionsVisited();
        }

        public void right(int magnitude) {
            headNode.right(magnitude);
        }

        public void left(int magnitude) {
            headNode.left(magnitude);
        }

        public void up(int magnitude) {
            headNode.up(magnitude);
        }

        public void down(int magnitude) {
            headNode.down(magnitude);
        }
    }
}

class HeadNode extends Node {
    private void vertical(boolean positive, int steps) {
        for (var i = 0; i < steps; i++) {
            if (positive)
                y++;
            else
                y--;
            move();
        }
    }

    private void horizontal(boolean positive, int steps) {
        for (var i = 0; i < steps; i++) {
            if (positive)
                x++;
            else
                x--;
            move();
        }

    }

    void down(int steps) {
        vertical(false, steps);
    }

    void up(int steps) {
        vertical(true, steps);
    }

    void right(int steps) {
        horizontal(true, steps);
    }

    void left(int steps) {
        horizontal(false, steps);
    }

    @Override
    void move() {
        next.move();
    }
}

class Node {
    protected int x, y;
    protected Node next, parent;

    private Set<Position> positions;

    void setNext(Node next) {
        this.next = next;
        next.parent = this;
    }

    void move() {

        if (Math.abs(parent.x - x) < 2 && Math.abs(parent.y - y) < 2)
            return;

        if (parent.x == x) {
            if (parent.y - y == 2)
                y++;
            else if (y - parent.y == 2)
                y--;
            else
                throw new RuntimeException("Invalid state y diff > 2");
        } else if (parent.y == y) {
            if (parent.x - x == 2)
                x++;
            else if (x - parent.x == 2)
                x--;
            else
                throw new RuntimeException("Invalid state x diff  2");
        } else {
            var right = parent.x > x;
            var up = parent.y > y;

            if (right)
                x++;
            else
                x--;

            if (up)
                y++;
            else
                y--;
        }

        if (next != null)
            next.move();
        else
            storePos();
    }

    void storePos() {
        if (positions == null) {
            positions = new HashSet<>();
            positions.add(new Position(0, 0));
        }

        positions.add(new Position(x, y));
    }

    int positionsVisited() {
        if (next != null) return -1;
        return positions.size();
    }

    record Position(int x, int y) {
    }
}


