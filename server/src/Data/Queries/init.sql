CREATE TABLE IF NOT EXISTS books (
    id INT GENERATED ALWAYS AS IDENTITY,
    isbn UUID NOT NULL UNIQUE,
    created_at TIMESTAMP NOT NULL DEFAULT NOW(),
    PRIMARY KEY(id)
);

-- snapshot table
CREATE TABLE IF NOT EXISTS book_snapshots (
    id INT GENERATED ALWAYS AS IDENTITY,
    book_id INT,
    title VARCHAR(255) NOT NULL,
    author VARCHAR(255) NOT NULL,
    pages INT NOT NULL CHECK (pages > 0),
    modified_at TIMESTAMP NOT NULL DEFAULT NOW(),
    PRIMARY KEY(id),
    CONSTRAINT fk_books
        FOREIGN KEY(book_id)
            REFERENCES books(id)
);

-- tombstone table
CREATE TABLE IF NOT EXISTS book_removed (
    id INT GENERATED ALWAYS AS IDENTITY,
    book_id INT UNIQUE,
    PRIMARY KEY(id),
    removed_at TIMESTAMP NOT NULL DEFAULT NOW(),
    CONSTRAINT fk_books
        FOREIGN KEY(book_id)
            REFERENCES books(id)
);
