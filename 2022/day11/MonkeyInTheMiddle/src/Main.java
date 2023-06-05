import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.function.Function;
import java.util.stream.Stream;

public class Main {
    public static void main(String[] args) {

        Function<Long, Long> worryChangeFunction = i -> i;
        new Monkey(
                0,
                new ArrayList<>(Stream.of(50, 70, 54, 83, 52, 78).toList()),
                o -> o * 3,
                11,
                worryChangeFunction,
                2,
                7
        );
        new Monkey(
                1,
                new ArrayList<>(Stream.of(71, 52, 58, 60, 71).toList()),
                o -> o * o,
                7,
                worryChangeFunction,
                0,
                2);
        new Monkey(
                2,
                new ArrayList<>(Stream.of(66, 56, 56, 94, 60, 86, 73).toList()),
                o -> o + 1,
                3,
                worryChangeFunction,
                7,
                5);
        new Monkey(
                3,
                new ArrayList<>(Stream.of(83, 99).toList()),
                o -> o + 8,
                5,
                worryChangeFunction,
                6,
                4);
        new Monkey(
                4,
                new ArrayList<>(Stream.of(98, 98, 79).toList()),
                o -> o + 3,
                17,
                worryChangeFunction,
                1,
                0);
        new Monkey(
                5,
                new ArrayList<>(List.of(76)),
                o -> o + 4,
                13,
                worryChangeFunction,
                6,
                3);
        new Monkey(
                6,
                new ArrayList<>(Stream.of(52, 51, 84, 54).toList()),
                o -> o * 17,
                19,
                worryChangeFunction,
                4,
                1);
        new Monkey(
                7,
                new ArrayList<>(Stream.of(82, 86, 91, 79, 94, 92, 59, 94).toList()),
                o -> o + 7,
                2,
                worryChangeFunction,
                5,
                3);

        var rounds = 10000;


        for (var i = 0; i < rounds; i++) {
            for (Monkey monkey : Monkey.all()) {
                monkey.inspectAndThrow();
            }
        }

        for (Monkey monkey : Monkey.all()) {
            System.out.println(monkey.getId() + " " + monkey.getItemsInspected());
        }

        var monkeyBusiness = Monkey.all()
                .stream().map(Monkey::getItemsInspected)
                .sorted()
                .skip(Monkey.all().size() - 2)
                .mapToLong(i -> i)
                .boxed()
                .reduce((a, b) -> a * b);

        System.out.println("Monkey business: " + monkeyBusiness.get());
    }
}


class Monkey {

    private static final List<Monkey> allMonkeys = new ArrayList<>(4);

    private static int moduloValue = 1;
    private final int id;
    private final List<Long> startingItems;
    private final Function<Long, Long> operation;
    private final int test;
    private final Function<Long, Long> worryChangeFunction;
    private final int passTrue;
    private final int passFalse;

    private int itemsInspected;


    Monkey(int number, List<Integer> startingItems, Function<Long, Long> operation, int test, Function<Long, Long> worryChangeFunction, int passTrue, int passFalse) {
        this.id = number;
        this.startingItems = new ArrayList<>(startingItems.stream().mapToLong(i -> i).boxed().toList());
        this.operation = operation;
        this.test = test;
        this.worryChangeFunction = worryChangeFunction;
        this.passTrue = passTrue;
        this.passFalse = passFalse;

        allMonkeys.add(number, this);
        moduloValue *= test;
    }

    public void inspectAndThrow() {
        for (Long startWorryLevel : startingItems) {
            var inspectWorryLevel = operation.apply(startWorryLevel);
            inspectWorryLevel = worryChangeFunction.apply(inspectWorryLevel);
            inspectWorryLevel = inspectWorryLevel % moduloValue;

            var recipient = (inspectWorryLevel % test) == 0 ? passTrue : passFalse;


            throwTo(recipient, inspectWorryLevel);
        }
        itemsInspected += startingItems.size();
        startingItems.clear();
    }

    public void throwTo(int receipient, Long item) {
        allMonkeys.get(receipient).startingItems.add(item);
    }

    public int getItemsInspected() {
        return itemsInspected;
    }

    public int getId() {
        return id;
    }

    public static List<Monkey> all() {
        return Collections.unmodifiableList(allMonkeys);
    }
}