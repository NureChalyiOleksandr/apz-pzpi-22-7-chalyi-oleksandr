#include <vector>
#include <map>
#include <string>
#include <memory>

struct Point {
    int x, y;
};

struct Constraint {
    std::string description;
};


// Memento
class ConstraintSolverMemento {
    friend class ConstraintSolver;
private:
    std::vector<Constraint> constraintsSnapshot;
    std::map<std::string, Point> objectPositions;

    ConstraintSolverMemento(
        const std::vector<Constraint>& constraints,
        const std::map<std::string, Point>& positions
    ) : constraintsSnapshot(constraints), objectPositions(positions) {
    }
};

// Originator
class ConstraintSolver {
public:
    void AddConstraint(const Constraint& c) {
        constraints.push_back(c);
    }

    void MoveObject(const std::string& id, const Point& delta) {
        objectPositions[id].x += delta.x;
        objectPositions[id].y += delta.y;
    }

    std::shared_ptr<ConstraintSolverMemento> CreateMemento() const {
        return std::make_shared<ConstraintSolverMemento>(constraints, objectPositions);
    }

    void Restore(std::shared_ptr<ConstraintSolverMemento> memento) {
        constraints = memento->constraintsSnapshot;
        objectPositions = memento->objectPositions;
    }

private:
    std::vector<Constraint> constraints;
    std::map<std::string, Point> objectPositions;
};

// Caretaker
class MoveCommand {
public:
    MoveCommand(ConstraintSolver& solver, const std::string& objectId, Point delta)
        : solver(solver), objectId(objectId), delta(delta) {
    }

    void Execute() {
        backup = solver.CreateMemento();
        solver.MoveObject(objectId, delta);
    }

    void Undo() {
        if (backup) {
            solver.Restore(backup);
        }
    }

private:
    ConstraintSolver& solver;
    std::string objectId;
    Point delta;
    std::shared_ptr<ConstraintSolverMemento> backup;
};
